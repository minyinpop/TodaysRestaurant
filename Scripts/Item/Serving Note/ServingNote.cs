using System.Collections.Generic;
using Common.Object.Storage_Slot.Type;
using UnityEngine;

namespace Item.Serving_Note
{
    public sealed class ServingNote : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private RectTransform parent;
        [field: SerializeField] private GameObject slotPrefab;

        private Queue<ServingNoteSlot> _servingNoteSlots = new();
    }
}