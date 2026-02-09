using System.Collections.Generic;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Information_UI.Child
{
    public sealed class LevelInformationSlotContainer : MonoBehaviour
    {
        [field: Header("Settings")]
        [field: SerializeField] private int maxSlotPerRow;
        
        private readonly Queue<LevelInformationSlot> _slots = new();

        public bool CanAddSlot()
        {
            return _slots.Count < maxSlotPerRow;
        }

        public void TryAddSlot(LevelInformationSlot slot)
        {
            if (slot == null) return;
            if (_slots.Count >= maxSlotPerRow) return;
            
            slot.transform.SetParent(transform);
            _slots.Enqueue(slot);
        }

        public void FullSlot(LevelInformationSlot slotPrefab)
        {
            for (var i = _slots.Count; i < maxSlotPerRow; i++)
            {
                var newSlot = Instantiate(slotPrefab, transform);
                _slots.Enqueue(newSlot);
            }
        }
    }
}