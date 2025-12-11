using Common.Object.Storage_Slot.Base;
using Item;
using Player_System.Data.Child.Inventory;
using UnityEngine;

namespace Player_System.Inventory.Child
{
    internal sealed class HotbarSystem : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private InventorySO InventoryData;
        
        [field: Header("Storage Slot")]
        [field: SerializeField] private StorageSlot[] StorageSlots;

        public void TryAddItem(ITem itemData, out bool isSuccess)
        {
            foreach (var storageSlot in StorageSlots)
            {
                storageSlot.TryAddItem(itemData, out isSuccess);
                if (isSuccess) return;
            }
            
            isSuccess = false;
        }
    }
}