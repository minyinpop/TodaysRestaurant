using System;
using System.General;
using Data.Item.Base;

namespace General.Object.Item_Slot.Base
{
    internal abstract class ItemSlot : PointerEvent
    {
        public event Action<ItemSlot, bool, ItemSO> OnClick;
        protected void OnClicked(bool onSelect, ItemSO itemData) { OnClick?.Invoke(this, onSelect, itemData); }

        protected override void OnPointerEnter() { }
        protected override void OnPointerExit() { }
        protected override void OnPointerClick() { }

        public virtual bool Add(ItemSO item) { return false; }

        public virtual void SetInteractable(bool interactable) { }
        public virtual void SetAlpha() { }
    }
}