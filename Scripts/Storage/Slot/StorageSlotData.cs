using Item;

namespace Storage.Slot
{
    /// <summary>
    /// 用來當作儲物格的資料，也可以當作儲物格之間傳遞資料的媒介。
    /// </summary>
    [System.Serializable]
    public struct StorageSlotData
    {
        // 該儲物格是否為上鎖的狀態。
        public bool isLocked;
        
        // 儲物格的物品的資料。
        public ItemData itemData;

        // 儲物格的物品的數量。
        public int itemQuantity;
    }
}
