using System.Storage_Slot.Base;
using Data.Item.Base;
using UnityEngine;

namespace System.Player.Inventory.Child
{
    internal sealed class HotbarSystem : MonoBehaviour
    {
        [field: Header("Storage Slot")]
        [field: SerializeField] private StorageSlot[] StorageSlots;

        [field: Header("Develop Only")]
        [field: SerializeField] private ItemSO[] DefaultItemsData;

        private void Start()
        {
            foreach (var itemData in DefaultItemsData) TryAddItem(itemData, out _);
        }

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