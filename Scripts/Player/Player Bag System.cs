using System.Collections.Generic;
using Storage;
using Storage.Slot;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Player
{
    public class PlayerBagSystem : MonoBehaviour
    {
        // 輸入系統
        private InputManager _input;
        
        // 物品欄介面暫存
        private GameObject _tempInventoryUI;
        // 背包介面暫存
        private GameObject _tempBagUI;
        
        // 儲存格站存
        private List<StorageSlotCore> _tempStorageSlots = new();
        
        [field: Header("資料"), Tooltip("玩家背包的資料庫組件"), SerializeField]
        public StorageDataSO StorageDataSO { get; private set; }
        
        [field: Header("介面"), Tooltip("物品欄介面的預製件"), SerializeField]
        private GameObject inventoryUIPrefab;
        
        [field: Tooltip("背包介面的預製件"), SerializeField]
        private GameObject bagUIPrefab;
        
        [field: Tooltip("畫布的位置組件"), SerializeField]
        private Canvas canvas;

        private void Awake()
        {
            _input = InputSystem.Input;
        }

        private void Start()
        {
            if (inventoryUIPrefab is not null)
            {
                _tempInventoryUI = Instantiate(inventoryUIPrefab, canvas.transform);

                foreach (var storageSlot in _tempInventoryUI.GetComponentsInChildren<StorageSlotCore>())
                    _tempStorageSlots.Add(storageSlot);
            }
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
        /// 背包互動鍵邏輯
        /// </summary>
        /// <param name="context"></param>
        private void OnOpenBagButtonPressed(InputAction.CallbackContext context)
        {
            if (_tempBagUI is null)
            {
                _tempBagUI = Instantiate(bagUIPrefab, canvas.transform);
                
                foreach (var storageSlot in _tempBagUI.GetComponentsInChildren<StorageSlotCore>())
                    _tempStorageSlots.Add(storageSlot);
                
                Refresh();
            }
            else
            {
                foreach (var storageSlot in _tempBagUI.GetComponentsInChildren<StorageSlotCore>())
                    _tempStorageSlots.Remove(storageSlot);
                
                Destroy(_tempBagUI);
                _tempBagUI = null;
            }
        }

        /// <summary>
        /// 更新背包邏輯
        /// </summary>
        private void Refresh()
        {
            var i = 0;
            
            foreach (var storageSlot in _tempStorageSlots)
            {
                storageSlot.Refresh(StorageDataSO.StorageSlotInfos[i]);
                i++;
            }
        }
    }
}
