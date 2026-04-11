using System;
using System.Collections.Generic;

namespace Common.Data_Saver.Player_Inventory_Saver.Child
{
    [Serializable]
    public sealed class InventorySaveData
    {
        public List<InventorySaveDataEntry> HotbarSlots;
        public List<InventorySaveDataEntry> BackpackSlots;
    }
}