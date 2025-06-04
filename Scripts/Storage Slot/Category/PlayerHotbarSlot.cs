using Item.Base;
using Storage_Slot.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage_Slot.Category
{
    internal class PlayerHotbarSlot : StorageSlotBase
    {
        [field: SerializeField] private Image ItemImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI ItemQuantityTMP { get; set; }
        private StorageSlotData SlotData { get; set; }

        internal override bool AddItem(ItemBase item)
        {
            if (item is null) return false;
            if (!SlotData.AddItem(item)) return false;
            return true;
        }

        internal override bool AddItem(ItemBase item, int quantity)
        {
            
        }
    }
}