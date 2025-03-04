using System.Collections.Generic;
using Character.Inventory_Space;
using Storage;
using Storage.Slot;
using UnityEngine;

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
        
        [Header("資料庫"), Tooltip("- 玩家的角色資料庫。\n- 用來判斷背包的儲物格開啟數量有多少。"), SerializeField]
        private CharacterInventorySpaceData characterDatabase;
        
        [Tooltip("- 儲物介面的資料庫陣列。\n- 排序會影響到物品添加的先後順序。"), SerializeField]
        private List<StorageData> storageDataList;

        private void Awake()
        {
            _tempInventoryUI = Instantiate(inventoryUIPrefab, uiSpawnPoint).GetComponent<StorageUI>();
        }
        
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

                // 判斷物品剩餘數量的邏輯。
                switch (remainingQuantity)
                {
                    case > 0:
                    {
                        print("還有物品沒被添加完畢。");
                        continue;
                    }
                    case < 0:
                    {
                        print("物品數量不能為負，有可能會出問題。");
                        return;
                    }
                    default:
                    {
                        print("物品添加完畢。");
                        
                        // 更新儲物介面。
                        _tempInventoryUI?.Refresh();
                        _tempBagUI?.Refresh();

                        return;
                    }
                }
            }
        }
    }
}
