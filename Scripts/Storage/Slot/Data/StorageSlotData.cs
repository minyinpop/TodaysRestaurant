using Item.Base;
using UnityEngine;

namespace Storage.Slot.Data
{
    [System.Serializable]
    internal class StorageSlotData
    {
        [field: SerializeField] internal ItemBase Item { get; private set; }
        [field: SerializeField] internal int Quantity { get; private set; }

        /// <summary>
        /// 設置該儲物格的物品與數量
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <param name="quantity"> 物品的數量 </param>
        /// <returns> 是否添加成功 </returns>
        internal bool SetItem(ItemBase item, int quantity)
        {
            if (Item is not null) return false;
            if (item is null) return false;
            Item = item;
            Quantity = quantity;
            return true;
        }

        /// <summary>
        /// 強制設置該儲物格的物品與數量
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <param name="quantity"> 物品的數量 </param>
        internal void ForceSetItem(ItemBase item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        /// <summary>
        /// 讓已儲存物品的數量多加 1
        /// </summary>
        /// <returns> 是否添加成功 </returns>
        internal bool AddItem()
        {
            if (Item is null) return false;
            if (!Item.GetStackSettings().CanStack) return false;
            Quantity++;
            return true;
        }
    }
}