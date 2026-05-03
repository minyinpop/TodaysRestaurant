using System;
using System.Collections.Generic;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.Object.Child
{
    public sealed class ItemGetSlotContainer : MonoBehaviour
    {
        [field: Header("Settings")]
        [field: SerializeField] private int maxSlotPerRow;
        
        private readonly Queue<ItemGetSlot> _slots = new();

        private void Awake()
        {
            if (maxSlotPerRow < 1)
            {
                // throw
            }
        }

        public bool HaveEmptySpace()
        {
            return _slots.Count < maxSlotPerRow;
        }

        public void AddSlot(ItemGetSlot slotPrefab, ItemSO item, out ItemGetSlot newSlot)
        {
            if (slotPrefab is null)
            {
                throw new ArgumentNullException($"{name} > {GetType().Name} > {nameof(AddSlot)} > {nameof(slotPrefab)} cannot be null.");
            }

            if (item is null)
            {
                throw new ArgumentNullException($"{name} > {GetType().Name} > {nameof(AddSlot)} > {nameof(item)} cannot be null.");
            }

            if (HaveEmptySpace())
            {
                newSlot = Instantiate(slotPrefab, transform);
                _slots.Enqueue(newSlot);
                
                newSlot.AddItem(item);
            }
            else
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} is full.");
            }
        }
    }
}