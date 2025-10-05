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

        public virtual bool Add(ItemSO item) { return false; }

        public virtual void Add(ItemSO item, Action onComplete) { onComplete?.Invoke(); }
        
        public virtual void Get(out ItemSO item) { item = null; }
        
        public virtual bool IsEmpty() { return true; }

        public virtual void OnClick(Action<ItemSO> onClick) { onClick?.Invoke(null); }
    }
}