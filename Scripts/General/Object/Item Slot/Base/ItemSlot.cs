using System;
using System.General;
using Data.Item.Base;

namespace General.Object.Item_Slot.Base
{
    internal abstract class ItemSlot : PointerEvent
    {
        public event Action<ItemSlot, ItemSO> OnClick;
        protected void OnClicked(ItemSO itemData) { OnClick?.Invoke(this, itemData); }

        protected override void OnPointerEnter() { }
        protected override void OnPointerExit() { }
        protected override void OnPointerClick() { }

        public virtual void SetSlotState(ItemSlotState slotState) { }
        public virtual void GetSlotState(out ItemSlotState slotState) { slotState = ItemSlotState.Lock; }

        public virtual void ChangeSelectState() { }

        public virtual bool Add(ItemSO item) { return false; }
        public virtual void Get(out ItemSO itemData) { itemData = null; }
        
        public virtual void SetAlpha() { }

        public virtual void Reset() { }
    }
}