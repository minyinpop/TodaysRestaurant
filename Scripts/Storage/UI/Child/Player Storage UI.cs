using Item.Core;
using Storage.Data.Core;
using Storage.Slot.Core;
using Storage.UI.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Storage.UI.Child
{
    public class PlayerStorageUI : StorageUICore
    {
        private InputManager _input;
        
        private GameObject _inventoryUI;
        private GameObject _bagUI;

        [field: Header("相關介面的預製件"), Tooltip("物品欄介面的預製件"), SerializeField]
        private GameObject inventoryUIPrefab;
        
        [field: Tooltip("背包介面的預製件"), SerializeField]
        private GameObject bagUIPrefab;

        [field: Header("相關介面的生成點"), Tooltip("背包介面的生成位置"), SerializeField]
        private Transform spawnPoint;

        private void Awake()
        {
            _input = InputSystem.Input;
        }

        private void Start()
        {
            _inventoryUI = Instantiate(inventoryUIPrefab, spawnPoint);

            foreach (var storageSlotCore in _inventoryUI.GetComponentsInChildren<StorageSlotCore>())
                StorageSlotCoreList.Add(storageSlotCore);
        }

        private void OnEnable()
        {
            _input.Player.OpenBag.started += OnOpenBagButtonPressed;
        }

        private void OnDisable()
        {
            _input.Player.OpenBag.started -= OnOpenBagButtonPressed;
        }

        /// <summary>
        /// 當 " Open Bag " 按鈕被按下時
        /// </summary>
        /// <param name="context"></param>
        private void OnOpenBagButtonPressed(InputAction.CallbackContext context)
        {
            if (_bagUI is null)
            {
                _bagUI = Instantiate(bagUIPrefab, spawnPoint);
                
                foreach (var storageSlotCore in _bagUI.GetComponentsInChildren<StorageSlotCore>())
                    StorageSlotCoreList.Add(storageSlotCore);
            }
            else
            {
                foreach (var storageSlotCore in _bagUI.GetComponentsInChildren<StorageSlotCore>())
                    StorageSlotCoreList.Remove(storageSlotCore);
                
                Destroy(_bagUI);
                _bagUI = null;
            }
        }

        /// <summary>
        /// 添加單個物品
        /// </summary>
        /// <param name="item"></param>
        public override void AddItem(ItemCore item)
        {
            for (var i = 0; i < StorageData.storageSlotDataList.Count; i++)
            {
                if (StorageData.storageSlotDataList[i].@lock == StorageSlotData.LockState.Yes)
                    continue;
                
                if (StorageData.storageSlotDataList[i].item is null)
                {
                    var newData = new StorageSlotData
                    {
                        @lock = StorageSlotData.LockState.No,
                        item = item,
                        itemAmount = StorageData.storageSlotDataList[i].itemAmount
                    };

                    StorageData.storageSlotDataList[i] = newData;
                    StorageSlotCoreList[i].Refresh(newData);
                    return;
                }

                if (StorageData.storageSlotDataList[i].itemAmount < StorageData.storageSlotDataList[i].item.MaxStack)
                {
                    var newData = new StorageSlotData
                    {
                        @lock = StorageSlotData.LockState.No,
                        item = item,
                        itemAmount = StorageData.storageSlotDataList[i].itemAmount + 1
                    };
                    
                    StorageData.storageSlotDataList[i] = newData;
                    StorageSlotCoreList[i].Refresh(newData);
                    return;
                }
            }
        }
    }
}
