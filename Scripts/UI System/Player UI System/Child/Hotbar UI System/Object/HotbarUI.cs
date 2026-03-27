using System.Collections.Generic;
using System.Linq;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using Common.Player.Child.Player_Inventory;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Hotbar_UI_System.Object
{
    public sealed class HotbarUI : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private PlayerInventorySO playerInventoryData;
        
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

        private void Start()
        {
            PerformHotbar(0);
        }

        #region Input
            public void UseSelectedHotbarSlotItem()
            {
                _selectedHotbarSlot?.Use();
            }
            
            public void PerformHotbar(int hotbarIndex)
            {
                ChangeSelectedHotbarSlot(_hotbarSlots[hotbarIndex]);
            }
        #endregion
        
        #region Item
            public bool TryAddItem(IItem itemData)
            {
                var result = _hotbarSlots.Any(slot => slot.AddItem(itemData));
                return result;
            }

            public bool TryRemoveItem(ItemSO itemData)
            {
                return _hotbarSlots.Any(hotbarSlot => hotbarSlot.TryRemoveItem(itemData));
            }
        #endregion
        
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