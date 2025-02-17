using System.Collections.Generic;
using Item.Interface;
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
        /// 添加物品到指定的儲物格。
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool AddItem(ITem item, int quantity)
        {
            // 剩餘的數量。
            var remainingQuantity = quantity;
            
            // TODO: 製作儲物格被鎖起來的判斷
            
            for (var i = 0; i < data.Count; i++)
            {
                // 如果儲物格是空的。
                if (data[i].Item is null)
                {
                    // 不能被放完。
                    if (remainingQuantity > item.MaxStack)
                    {
                        remainingQuantity -= item.MaxStack;
                        
                        data[i] = new StorageSlotData
                        {
                            Locked = data[i].Locked,
                            Item = item,
                            Quantity = item.MaxStack
                        };
                        continue;
                    }

                    // 能被放完。
                    data[i] = new StorageSlotData
                    {
                        Locked = data[i].Locked,
                        Item = item,
                        Quantity = remainingQuantity
                    };
                    return true;
                }
                
                // TODO: 製作儲物格被佔領的判斷
            }
            return false;
        }
    }
}