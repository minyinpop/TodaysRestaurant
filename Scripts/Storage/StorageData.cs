using System.Collections.Generic;
using Item;
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
        /// <returns> 返還沒有被添加完的物品的數量。 </returns>
        public int AddItem(StorageSlotData newSlotData)
        {
            // 被添加的物品的資料。
            var newItemData = newSlotData.itemData;
            
            // 用來記錄物品剩餘數量的參數。
            var remainingQuantity = newSlotData.itemQuantity;

            for (var i = 0; i < slotDataList.Count; i++)
            {
                // 第 i 格的儲物個的物品資料。
                var nowItemData = slotDataList[i].itemData;

                // 第 i 格的儲物格的物品數量。
                var nowItemQuantity = slotDataList[i].itemQuantity;

                // 如果第 i 格的儲物格是鎖上的，就切換到下一個儲物格做判斷。
                if (slotDataList[i].isLocked)
                    continue;

                // 如果第 i 格的儲物格是空的，就直接把物品給添加去。
                if (nowItemData is null)
                {
                    // 如果第 i 格的儲物格不能容納所有被添加的物品。
                    if (remainingQuantity >= newItemData.MaxStack)
                    {
                        slotDataList[i] = UpdateSlotData(newItemData, newItemData.MaxStack);
                        remainingQuantity -= newItemData.MaxStack;
                    }
                    // 如果第 i 格的儲物格可以容納所有被添加的物品。
                    else
                    {
                        slotDataList[i] = UpdateSlotData(newItemData, remainingQuantity);
                        return 0;
                    }
                }
                // 如果第 i 格的儲物格是有東西的，就比較被添加的物品與儲物格裡的物品是否一致，再做後續的判斷與添加。
                else
                {
                    // 如果被添加的物品與第 i 格的儲物格的物品不是一致的。
                    if (!newItemData.Equals(nowItemData))
                        continue;

                    // 第 i 格的儲物格的物品是不能堆疊的。
                    if (nowItemData.Stackable == false)
                        continue;

                    // 第 i 格的儲物格已經堆滿。
                    if (nowItemQuantity >= nowItemData.MaxStack)
                        continue;

                    // 第 i 格的儲物格可以一次容納所有的物品。
                    if (remainingQuantity + nowItemQuantity <= nowItemData.MaxStack)
                    {
                        slotDataList[i] = UpdateSlotData(nowItemData, remainingQuantity + nowItemQuantity);
                        return 0;
                    }

                    // 第 i 格的儲物格不能一次容納所有的物品。
                    slotDataList[i] = UpdateSlotData(nowItemData, nowItemData.MaxStack);
                    remainingQuantity -= nowItemData.MaxStack - nowItemQuantity;
                }
            }

            // 返還沒有被添加完的物品的數量。
            return remainingQuantity;
        }

        /// <summary>
        /// 用來更新儲物格的資料。
        /// </summary>
        /// <param name="itemData"> 儲物格的物品的資料。 </param>
        /// <param name="itemQuantity"> 儲物格的物品的數量。 </param>
        /// <returns> 回傳新的儲物格資料。 </returns>
        private static StorageSlotData UpdateSlotData(ItemData itemData, int itemQuantity)
        {
            return new StorageSlotData
            {
                isLocked = false,
                itemData = itemData,
                itemQuantity = itemQuantity,
            };
        }
    }
}
