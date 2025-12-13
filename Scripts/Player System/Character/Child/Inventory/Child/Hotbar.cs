using Common.Object.Storage_Slot.Type;
using Item;
using Player_System.Data.Child.Inventory;
using UnityEngine;

namespace Player_System.Character.Child.Inventory.Child
{
    internal sealed class Hotbar : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private InventorySO InventoryData;
        
        [field: Header("Storage Slot")]
        [field: SerializeField] private HotbarSlot[] HotbarSlots;
        
        [field: SerializeField] private HotbarSlot SelectedHotbarSlot; // TODO Develop Only
        // private HotbarSlot SelectedHotbarSlot;

        public void TryAddItem(ITem itemData, out bool isSuccess)
        {
            foreach (var hotbarSlot in HotbarSlots)
            {
                hotbarSlot.TryAddItem(itemData, out isSuccess);
                if (isSuccess) return;
            }
            
            isSuccess = false;
        }

        public void OnPerformedHotbar(int hotbarIndex)
        {
            SelectedHotbarSlot = HotbarSlots[hotbarIndex];
            SelectedHotbarSlot.OnSelected();
        }
    }
}