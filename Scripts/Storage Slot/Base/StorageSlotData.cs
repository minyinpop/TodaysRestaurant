using Item.Base;
using UnityEngine;

namespace Storage_Slot.Base
{
    [System.Serializable]
    internal class StorageSlotData
    {
        [field: SerializeField] private ItemBase Item { get; set; }
        [field: SerializeField] private int Quantity { get; set; }

        /// <summary>
        /// 添加 1 個物品，並且檢查該儲物格是否可以被添加
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <returns> 回傳是否添加成功 </returns>
        internal bool AddItem(ItemBase item)
        {
            if (Item is null)
            {
                Item = item;
                Quantity = 1;
            }
            else
            {
                if (Item != item) return false;
                if (!item.GetStackSettings().CanStack) return false;
                if (Quantity >= Item.GetStackSettings().MaxStack) return false;
                Item = item;
                Quantity += 1;
            }
            
            return true;
        }
    }
}