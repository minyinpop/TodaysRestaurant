using System.Collections.Generic;
using Common.Object.Storage_Slot.Type;
using Item;
using Item.Serving_Note;
using UnityEngine;

namespace UI_System.System.Child.Serving_Note_UI_System
{
    public sealed class ServingNoteUI : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private RectTransform slotParent;
        [field: SerializeField] private GameObject slotPrefab;

        private readonly List<ServingNoteSlot> _servingNoteSlots = new();
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
                _servingNoteSlots.Add(newSlot);
            }
        }

        public void GetSlotItems(out List<ItemSO> orderedItems)
        {
            orderedItems = new List<ItemSO>();
            
            foreach (var slot in _servingNoteSlots)
            {
                slot.TryPeekItem(out var slotItemData);
                orderedItems.Add(slotItemData);
            }
        }
    }
}