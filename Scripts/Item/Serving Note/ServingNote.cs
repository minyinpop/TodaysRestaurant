using System.Collections.Generic;
using Common.Object.Storage_Slot.Type;
using UnityEngine;

namespace Item.Serving_Note
{
    public sealed class ServingNote : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private RectTransform slotParent;
        [field: SerializeField] private GameObject slotPrefab;

        private readonly Queue<ServingNoteSlot> _servingNoteSlots = new();
        private ServingNoteSO _servingNoteData;

        public void Initialize(ServingNoteSO servingNoteData)
        {
            if (_servingNoteData is not null) return;
            _servingNoteData = servingNoteData;

            _servingNoteData.GetOrderedItems(out var orderedItems);
            foreach (var orderedItem in orderedItems)
            {
                var newSlot = Instantiate(slotPrefab, slotParent).GetComponent<ServingNoteSlot>();
                newSlot.Initialize(orderedItem);
                _servingNoteSlots.Enqueue(newSlot);
            }
        }
    }
}