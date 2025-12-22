using System.Collections.Generic;
using System.Linq;
using Common.Object.Storage_Slot.Type;
using Item;
using Player_System.Data.Child.Inventory;
using UnityEngine;

namespace UI_System.System.Child.Hotbar_UI_System
{
    public sealed class HotbarUI : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private InventorySO inventoryData;
        
        [field: Header("Components")]
        [field: SerializeField] private RectTransform hotbarSlotParent;

        private readonly List<HotbarSlot> _hotbarSlots = new();
        private HotbarSlot _selectedHotbarSlot;

        private void Awake()
        {
            for (var i = 0; i < hotbarSlotParent.childCount; i++)
            {
                var currentSlot = hotbarSlotParent.GetChild(i);
                _hotbarSlots.Add(currentSlot.GetComponent<HotbarSlot>());
                currentSlot.gameObject.SetActive(true);
            }
        }
        
        public bool TryAddItem(ItemSO itemData)
        {
            return _hotbarSlots.Any(slot => slot.TryAddItem(itemData));
        }
        
        public void PerformHotbar(int hotbarIndex)
        {
            ChangeSelectedHotbarSlot(_hotbarSlots[hotbarIndex]);
        }

        public void ClickRightButton()
        {
            _selectedHotbarSlot?.Use();
        }

        #region Utility
            private void ChangeSelectedHotbarSlot(HotbarSlot newSlot)
            {
                _selectedHotbarSlot?.UnSelected();
                _selectedHotbarSlot = null;
                _selectedHotbarSlot = newSlot;
                _selectedHotbarSlot?.Selected();
            }
        #endregion
    }
}