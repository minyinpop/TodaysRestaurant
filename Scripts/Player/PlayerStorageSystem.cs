using System.Collections.Generic;
using Storage;
using Storage.Slot;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Player
{
    /// <summary>
    /// 用來控制玩家儲物系統的類。
    /// </summary>
    public class PlayerStorageSystem : MonoBehaviour
    {
        [Header("位置"), Tooltip("介面要生成在哪一個遊戲物件的底下。"), SerializeField]
        private Transform uiSpawnPoint;
        
        [Header("儲物介面"), Tooltip("玩家快捷欄的儲物介面的預製件。"), SerializeField]
        private GameObject inventoryUIPrefab;
        
        // 用來暫存玩家快捷欄的遊戲物件。
        private StorageUI _tempInventoryUI;
        
        [Tooltip("玩家背包的儲物介面的預製件。"), SerializeField]
        private GameObject bagUIPrefab;
        
        // 用來暫存玩家背包的遊戲物件。
        private StorageUI _tempBagUI;
        
        [Tooltip("- 儲物介面的資料庫陣列。\n- 排序會影響到物品添加的先後順序。"), SerializeField]
        private List<StorageData> storageDataList;
        
        // 輸入端。
        private InputMap _input;

        private void Awake()
        {
            _input = InputSystem.input;
        }

        private void Start()
        {
            _tempInventoryUI = Instantiate(inventoryUIPrefab, uiSpawnPoint).GetComponent<StorageUI>();
            _tempInventoryUI.Refresh();
        }

        private void OnEnable()
        {
            _input.Player.Bag.performed += OnBagButtonDown;
        }

        private void OnDisable()
        {
            _input.Player.Bag.performed -= OnBagButtonDown;
        }

        /// <summary>
        /// 以 Storage Data List 的順序來添加物品。
        /// </summary>
        /// <param name="newSlotData"> 新傳入的儲物格資訊。 </param>
        public void AddItem(StorageSlotData newSlotData)
        {
            // 用來記錄物品剩餘數量的參數。
            var remainingQuantity = newSlotData.itemQuantity;

            foreach (var storageData in storageDataList)
            {
                var slotData = new StorageSlotData
                {
                    isLocked = newSlotData.isLocked,
                    itemData = newSlotData.itemData,
                    itemQuantity = remainingQuantity
                };
                remainingQuantity = storageData.AddItem(slotData);

                // 還有物品沒有被添加完畢，換到下一個 storageDataList 的儲物介面的資料庫。
                if (remainingQuantity > 0)
                    continue;

                // 物品數量不能是負的。
                if (remainingQuantity < 0)
                    return;

                _tempInventoryUI?.Refresh();
                _tempBagUI?.Refresh();
                return;
            }
        }

        /// <summary>
        /// 當 開啟背包 所設定的按鍵被按下時，所發生的事情。
        /// </summary>
        /// <param name="context"> 輸入系統的狀態。 </param>
        private void OnBagButtonDown(InputAction.CallbackContext context)
        {
            // 判斷背包是否為關閉的狀態，進而做出不同的操作。
            if (_tempBagUI is null)
            {
                _tempBagUI = Instantiate(bagUIPrefab, uiSpawnPoint).GetComponent<StorageUI>();
                _tempBagUI.Refresh();
            }
            else
            {
                Destroy(_tempBagUI.gameObject);
                _tempBagUI = null;
            }
        }
    }
}
