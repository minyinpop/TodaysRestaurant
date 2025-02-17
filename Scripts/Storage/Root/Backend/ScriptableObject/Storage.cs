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
        /// <returns></returns>
        public bool AddItem(ITem item, int quantity)
        {
            // 用來紀錄物品數量減去物品設定的最大堆疊數。
            var remainingQuantity = quantity - item.MaxStack;
            
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].Item is null)
                {
                    data[i] = new StorageSlotData
                    {
                        Locked = data[i].Locked,
                        Item = item,
                        Quantity = quantity
                    };
                    return true;
                }
            }
            return false;
        }
    }
}