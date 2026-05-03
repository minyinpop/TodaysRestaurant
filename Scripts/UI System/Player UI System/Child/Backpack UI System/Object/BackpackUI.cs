using System;
using System.Collections.Generic;
using System.Linq;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Backpack_UI_System.Object
{
    public sealed class BackpackUI : MonoBehaviour
    {
        [field: Header("物品格子")]
        [field: SerializeField] private BackpackSlot[] backpackSlots;
                                public IReadOnlyList<BackpackSlot> BackpackSlots => backpackSlots;
        private void OnEnable()
        {
            foreach (var slot in backpackSlots)
            {
                slot.Refresh();
            }
        }

        public bool AddItem(IItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = backpackSlots.Any(slot => slot.AddItem(item));
            return result;
        }
        
        public bool SearchAndRemoveItem(ItemSO itemData)
        {
            if (itemData is null)
            {
                throw new ArgumentNullException(nameof(itemData), "不能為空值。");
            }
                
            foreach (var backpackSlot in backpackSlots)
            {
                if (!ReferenceEquals(backpackSlot.Item, itemData))
                {
                    continue;
                }

                backpackSlot.RemoveItem();
                return true;
            }

            return false;
        }
        
        public void RemoveAllItems()
        {
            foreach (var backpackSlot in backpackSlots)
            {
                backpackSlot.RemoveItem();
            }
        }
    }
}