using System.Collections.Generic;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using Common.Item.Data.Serving_Note;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Serving_Note_UI_System.Object
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
            
            foreach (var orderedItem in _servingNoteData.OrderedItems)
            {
                var newSlot = Instantiate(slotPrefab, slotParent).GetComponent<ServingNoteSlot>();
                newSlot.Initialize(orderedItem);
                _servingNoteSlots.Add(newSlot);
            }
        }

        public void GetSlotItems(out List<IItem> orderedItems)
        {
            orderedItems = new List<IItem>();
            
            foreach (var slot in _servingNoteSlots)
            {
                orderedItems.Add(slot.Item);
            }
        }
    }
}