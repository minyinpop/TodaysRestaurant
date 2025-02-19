using System.Collections.Generic;
using Storage.Root.Backend.Struct;
using UnityEngine;

namespace Storage.Root.Backend.ScriptableObject
{
    [CreateAssetMenu(menuName = "Minyinpop/Storage", fileName = "New Storage", order = 1)]
    public class Storage : UnityEngine.ScriptableObject
    {
        // 物品添加的狀況。
        public enum AddItemResult
        {
            Fail = 0,
            Finish = 1,
            Remaining = 2
        }

        [Tooltip("儲物格的資料，\n順序會影響到儲物格的顯示。\n有多少儲物格就要創建多少筆資料。")]
        public List<StorageSlotData> data;

        public AddItemResult AddItem(int index, StorageSlotData newData)
        {
            // 如果這個儲物格是鎖起來的。
            if (data[index].Locked)
                return AddItemResult.Fail;
            
            // 如果這格儲物格是空的。
            if (data[index].Item is null)
            {
                // 如果物品數量大於最大的可堆疊數。
                if (newData.Quantity > newData.Item.MaxStack)
                {
                    data[index] = new StorageSlotData
                    {
                        Locked = newData.Locked,
                        Item = newData.Item,
                        Quantity = newData.Item.MaxStack
                    };
                    return AddItemResult.Remaining;
                }
                // 直接添加物品進空的儲物格。
                data[index] = newData;
                return AddItemResult.Finish;
            }
            // TODO: 撰寫這格儲物格已經有物品的程式碼。
            return AddItemResult.Fail;
        }
    }
}