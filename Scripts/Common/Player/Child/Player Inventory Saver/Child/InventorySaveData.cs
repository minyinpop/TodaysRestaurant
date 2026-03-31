using System.Collections.Generic;

namespace Common.Player.Child.Player_Inventory_Saver.Child
{
    [System.Serializable]
    public sealed class InventorySaveData
    {
        public List<InventorySaveDataEntry> HotbarSlots;
        public List<InventorySaveDataEntry> BackpackSlots;
    }
}