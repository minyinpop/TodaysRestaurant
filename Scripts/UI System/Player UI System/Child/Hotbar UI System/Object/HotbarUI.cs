using System;
using System.Collections.Generic;
using System.Linq;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Hotbar_UI_System.Object
{
    public sealed class HotbarUI : MonoBehaviour
    {
        [field: Header("Item Slot")]
        [field: SerializeField] private HotbarSlot[] hotbarSlots;
                                public IReadOnlyList<HotbarSlot> HotbarSlots => hotbarSlots;
        
        private HotbarSlot _selectedHotbarSlot;

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
                ChangeSelectedHotbarSlot(hotbarSlots[hotbarIndex]);
            }
        #endregion
        
        #region Item
            public bool AddItem(IItem itemData)
            {
                var result = hotbarSlots.Any(slot => slot.AddItem(itemData));
                return result;
            }

            public bool SearchAndRemoveItem(ItemSO itemData)
            {
                if (itemData is null)
                {
                    throw new ArgumentNullException(nameof(itemData), "不能為空值。");
                }
                
                foreach (var hotbarSlot in hotbarSlots)
                {
                    if (!ReferenceEquals(hotbarSlot.Item, itemData))
                    {
                        continue;
                    }

                    hotbarSlot.RemoveItem();
                    return true;
                }

                return false;
            }

            public void RemoveAllItems()
            {
                foreach (var hotbarSlot in hotbarSlots)
                {
                    hotbarSlot.RemoveItem();
                }
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