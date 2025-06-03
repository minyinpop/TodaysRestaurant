using Item;
using Item.Base;

namespace Storage.Slot.Data
{
    [System.Serializable]
    internal class StorageSlotData
    {
        internal ITem Item { get; private set; }
        internal int Quantity { get; private set; }

        /// <summary>
        /// 用於設置該儲物格物品與數量的方法
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <param name="quantity"> 物品的數量 </param>
        /// <returns> 是否添加成功 </returns>
        internal bool SetItem(ITem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
            return true;
        }
    }
}