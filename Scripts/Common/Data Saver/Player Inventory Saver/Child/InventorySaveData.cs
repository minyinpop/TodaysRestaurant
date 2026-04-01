using System.Collections.Generic;

namespace Common.Data_Saver.Player_Inventory_Saver.Child
{
    [System.Serializable]
    public sealed class InventorySaveData
    {
        public List<InventorySaveDataEntry> HotbarSlots;
        public List<InventorySaveDataEntry> BackpackSlots;
    }
}