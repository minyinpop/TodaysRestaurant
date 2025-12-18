using Common.Object.Storage_Slot;
using UnityEngine;

namespace Item.Serving_Note
{
    public sealed class ServingNote : MonoBehaviour
    {
        [field: Header("Storage Slots")]
        [field: SerializeField] private StorageSlot[] StorageSlots;
    }
}