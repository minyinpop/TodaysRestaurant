using System.Collections.Generic;
using Storage.Slot;
using UnityEngine;

namespace Storage
{
    /// <summary>
    /// 用來控制儲物介面的資料庫。
    /// </summary>
    [CreateAssetMenu(fileName = "New Storage Data", menuName = "New Storage Data", order = 1)]
    public class StorageData : ScriptableObject
    {
        [Header("資料庫"), Tooltip("- 物品的儲物格資料的陣列。\n- 排序會影響到物品添加的先後順序。\n- 有多少的儲物格就要添加多少筆的資料。"), SerializeField]
        private List<StorageSlotData> slotDataList;

        /// <summary>
        /// 使用儲存格資料來添加物品，
        /// </summary>
        /// <param name="newSlotData"> 要被添加的物品的資料。 </param>
        /// <returns> 返還物品的數量。 </returns>
        public int AddItem(StorageSlotData newSlotData)
        {
            // 用來記錄物品剩餘數量的參數。
            var remainingQuantity = newSlotData.itemQuantity;

            for (var i = 0; i < slotDataList.Count; i++)
            {
                // 如果第 i 格的儲物格是鎖上的，就切換到下一個儲物格做判斷。
                if (slotDataList[i].locked)
                    continue;
                
                // 如果第 i 格的儲物格是空的，就直接把物品給添加去。
                if (slotDataList[i].itemData is null)
                {
                    if (remainingQuantity > newSlotData.itemData.MaxStack)
                    {
                        // TODO: 繼續撰寫添加物品的邏輯 ......
                    }
                }
            }

            return 0;
        }
    }
}
