using Common.Object.Storage_Slot.Type;
using Item.Data;
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

        public void TryAddItem(ItemSO itemData, out bool isSuccess)
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
            ChangeSelectedHotbarSlot(HotbarSlots[hotbarIndex]);
        }

        public void OnClickedMouseLeftButton()
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