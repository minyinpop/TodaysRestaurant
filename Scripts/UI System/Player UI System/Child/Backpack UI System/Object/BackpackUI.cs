using System;
using System.Collections.Generic;
using System.Linq;
using Audio_System.Data;
using Audio_System.Main;
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
                                
        [field: Header("音效")]
        [field: SerializeField] private PlaySFXData openBackpackSFX;
        [field: SerializeField] private PlaySFXData closeBackpackSFX;

        private void OnEnable()
        {
            AudioSystem.Instance.UISFX.PlayOneShot(openBackpackSFX);
            
            foreach (var slot in backpackSlots)
            {
                slot.Refresh();
            }
        }
        
        private void OnDisable()
        {
            AudioSystem.Instance.UISFX.PlayOneShot(closeBackpackSFX);
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
    }
}