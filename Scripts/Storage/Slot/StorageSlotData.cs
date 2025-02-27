using Item;

namespace Storage.Slot
{
    [System.Serializable]
    public struct StorageSlotData
    {
        // 該儲物格是否為上鎖的狀態。
        public bool locked;
        
        // 儲物格的物品的資料。
        public ItemData itemData;

        // 儲物格的物品的數量。
        public int itemQuantity;
    }
}
