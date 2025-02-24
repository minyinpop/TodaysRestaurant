using System.Collections.Generic;
using Storage.Root.Backend.Struct;
using UnityEngine;

namespace Storage.Root.Backend.ScriptableObject
{
    [CreateAssetMenu(menuName = "Minyinpop/Storage", fileName = "New Storage", order = 1)]
    public class Storage : UnityEngine.ScriptableObject
    {
        [Tooltip("儲物格的資料，\n順序會影響到儲物格的顯示。\n有多少儲物格就要創建多少筆資料。")]
        public List<StorageSlotData> data;

        /// <summary>
        /// 判斷被添加的物品能否添加進儲物格，並且返回剩餘數量給呼叫的程式碼去做判斷。
        /// </summary>
        /// <param name="newData"> 被添加的物品的資料。 </param>
        /// <returns> 返還剩餘的物品數量。 </returns>
        public int AddItem(StorageSlotData newData)
        {
            for (var i = 0; i < data.Count; i++)
            {
                // 判斷該儲物格有沒有鎖起來。
                if (data[i].Locked)
                    continue;
                // 該儲物格內沒有物品。
                if (data[i].Item is null)
                {
                    // 該儲物格不能容納所有的物品。
                    if (newData.Quantity > newData.Item.MaxStack)
                    {
                        newData.Quantity -= newData.Item.MaxStack;

                        data[i] = new StorageSlotData
                        {
                            Locked = false,
                            Item = newData.Item,
                            Quantity = newData.Item.MaxStack
                        };
                    }
                    // 該儲物格可以容納所有的物品。
                    else
                    {
                        data[i] = new StorageSlotData
                        {
                            Locked = false,
                            Item = newData.Item,
                            Quantity = newData.Quantity
                        };
                        return 0;
                    }
                }
                // 該儲物格內有物品。
                else
                {
                    // TODO: 判斷該儲物格內有物品時的邏輯。
                }
            }
            
            return 0;
        }
    }
}