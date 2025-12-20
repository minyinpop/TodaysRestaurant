using System.Linq;
using Common.Object.Storage_Slot.Type;
using Item;
using Player_System.Data.Child.Inventory;
using UnityEngine;

namespace Player_System.System.Child.Inventory.Child
{
    internal sealed class Hotbar : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private InventorySO InventoryData;
        
        [field: Header("Storage Slot")]
        [field: SerializeField] private HotbarSlot[] HotbarSlots;
        
        private HotbarSlot SelectedHotbarSlot;

        public bool TryAddItem(ItemSO itemData)
        {
            if (HotbarSlots.Any(hotbarSlot => hotbarSlot.TryAddItem(itemData)))
            {
                return true;
            }

            return false;
        }

        public void OnPerformedHotbar(int hotbarIndex)
        {
            ChangeSelectedHotbarSlot(HotbarSlots[hotbarIndex]);
        }

        public void OnClickedLeftButton()
        {
            SelectedHotbarSlot?.Use();
        }

        #region Utility
            private void ChangeSelectedHotbarSlot(HotbarSlot newSlot)
            {
                SelectedHotbarSlot?.UnSelected();
                SelectedHotbarSlot = null;
                SelectedHotbarSlot = newSlot;
                SelectedHotbarSlot?.Selected();
            }
        #endregion
    }
}