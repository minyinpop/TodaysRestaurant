using System.Collections.Generic;
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
        [Header("儲物介面"), Tooltip("玩家快捷欄的儲物介面的預製件。"), SerializeField]
        private GameObject inventoryUIPrefab;
        
        [Tooltip("玩家背包的儲物介面的預製件。"), SerializeField]
        private GameObject bagUIPrefab;
        
        [Header("資料庫"), Tooltip("- 儲物介面的資料庫陣列。\n- 排序會影響到物品添加的先後順序。"), SerializeField]
        private List<StorageData> storageDataList;

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
                
                // TODO: 更新前台 UI ......
                
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
                        break;
                    }
                }
            }
        }
    }
}
