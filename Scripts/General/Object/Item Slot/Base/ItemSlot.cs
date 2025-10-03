using System;
using System.General;
using Data.Item.Base;
using UnityEngine;

namespace General.Object.Item_Slot.Base
{
    [RequireComponent(typeof(DoAnimation))]
    internal abstract class ItemSlot : PointerEvent
    {
        protected override void OnPointerEnter() { }
        protected override void OnPointerExit() { }
        protected override void OnPointerClick() { }

        public virtual bool Add(ItemSO item, Action onComplete) { return false; }
    }
}