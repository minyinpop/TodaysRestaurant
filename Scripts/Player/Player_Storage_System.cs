using Storage.Root.Backend.Struct;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Player
{
    public class PlayerStorageSystem : MonoBehaviour
    {
        // 輸入端的程式碼。
        private Input_Manager _input;

        
        
        [Header("生成點")]
        [Tooltip("儲物介面的生成位置。")]
        [SerializeField]
        private RectTransform spawnPoint;
        
        
        
        [Header("物品欄的儲物介面")]
        [Tooltip("物品欄儲物介面的資料庫。")]
        [SerializeField]
        private Storage.Root.Backend.ScriptableObject.Storage inventoryStorageData;
        
        [Tooltip("物品欄的儲物介面的預製件。")]
        [SerializeField]
        private GameObject inventoryUI;
        
        // 物品欄的儲物介面的暫存。
        private GameObject _tempInventoryUI;
        
        // 物品欄的儲物介面的程式碼。
        private Storage.Root.Frontend.Storage _tempInventoryStorage;
        
        
        
        [Header("背包的儲物介面")]
        [Tooltip("背包儲物介面的資料庫。")]
        [SerializeField]
        private Storage.Root.Backend.ScriptableObject.Storage bagStorageData;
        
        [Tooltip("背包的儲物介面的預製件。")]
        [SerializeField]
        private GameObject bagUI;
        
        // 背包的儲物介面的暫存。
        private GameObject _tempBagUI;
        
        // 背包的儲物介面的程式碼。
        private Storage.Root.Frontend.Storage _tempBagStorage;
        
        private void Awake()
        {
            _input = InputSystem.input;
            
            // 物品欄的儲物介面的初始設定。
            _tempInventoryUI = Instantiate(inventoryUI, spawnPoint);
            _tempInventoryStorage = _tempInventoryUI.GetComponent<Storage.Root.Frontend.Storage>();
            // 更新物品欄的儲物介面。
            _tempInventoryStorage.Refresh();
            
            // 載入當前解鎖的背包儲物格的數量，並且運用到背包的儲物介面的資料庫當中。
            for (var i = 0; i < PlayerStaticValue.BagUnlockedSlotPerLevel[PlayerStaticValue.BagLevelIndex]; i++)
            {
                var slotData = bagStorageData.slotDataList[i];
                
                bagStorageData.slotDataList[i] = new StorageSlotData
                {
                    Locked = false,
                    Item = slotData.Item,
                    Quantity = slotData.Quantity
                };
            }
        }

        private void OnEnable()
        {
            _input.Player.Bag.performed += OpenBag;
        }

        private void OnDisable()
        {
            _input.Player.Bag.performed -= OpenBag;
        }

        /// <summary>
        /// 當玩家按下 " Bag " 定義的按鍵所發生的事情。
        /// </summary>
        /// <param name="context"></param>
        private void OpenBag(InputAction.CallbackContext context)
        {
            // 如果背包的儲物介面是關閉的。
            if (_tempBagUI is null)
            {
                // 背包的儲物介面的初始設定。
                _tempBagUI = Instantiate(bagUI, spawnPoint);
                _tempBagStorage = _tempBagUI.GetComponent<Storage.Root.Frontend.Storage>();
                // 更新背包的儲物介面。
                _tempBagStorage.Refresh();
            }
            // 如果背包的儲物介面是開啟的。
            else
            {
                _tempBagStorage = null;
                
                Destroy(_tempBagUI);
                _tempBagUI = null;
            }
        }

        /// <summary>
        /// 添加物品到玩家的背包。
        /// </summary>
        /// <param name="newData"> 被添加的物品的資料。 </param>
        public void AddItem(StorageSlotData newData)
        {
            var data = newData;
            // 物品欄的儲物介面添加物品。
            var remainingQuantity = inventoryStorageData.AddItem(data);

            data = new StorageSlotData
            {
                Locked = false,
                Item = data.Item,
                Quantity = remainingQuantity
            };
            // 背包的儲物介面添加物品。
            remainingQuantity = bagStorageData.AddItem(data);

            // 表示物品添加完畢。
            if (remainingQuantity == 0)
            {
                Debug.Log("物品添加完畢。");

                // 更新儲物介面。
                _tempInventoryStorage?.Refresh();
                _tempBagStorage?.Refresh();
                return;
            }

            // 表示物品還有剩餘。
            if (remainingQuantity > 0)
            {
                Debug.Log("物品添加不完。");
                return;
            }

            Debug.LogWarning("被添加的物品數量不能為負數，發生錯誤 !");
        }
    }
}