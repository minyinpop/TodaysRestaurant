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
        /// 添加 1 個物品，並且會返回是否添加成功
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <returns> 添加是否成功 </returns>
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

        /// <summary>
        /// 添加數個物品，並且會返回是否添加成功
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <param name="quantity"> 物品的數量 </param>
        /// <param name="remaining"> 返回剩餘的數量 </param>
        /// /// <returns> 添加是否成功 </returns>
        internal bool AddItem(ItemBase item, int quantity, out int remaining)
        {
            if (Item is null)
            {
                Item = item;
            }

            remaining = quantity;
            return true;
        }
    }
}