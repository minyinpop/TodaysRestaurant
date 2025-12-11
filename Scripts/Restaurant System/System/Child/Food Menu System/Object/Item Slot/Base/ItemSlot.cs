using System;
using Common;
using Item;

namespace Restaurant_System.System.Child.Food_Menu_System.Object.Item_Slot.Base
{
    internal abstract class ItemSlot : PointerEvent
    {
        public event Action<ItemSlot, ITem> OnClick;
        protected void OnClicked(ITem itemData) { OnClick?.Invoke(this, itemData); }

        protected override void OnPointerEnter() { }
        protected override void OnPointerExit() { }
        protected override void OnPointerClick() { }

        public virtual void SetSlotState(ItemSlotState slotState) { }
        public virtual void GetSlotState(out ItemSlotState slotState) { slotState = ItemSlotState.Lock; }
        
        public virtual void ChangeSelectState() { }

        public virtual void Add(ITem item) { }
        public virtual void Add(ITem item, out bool isSuccess) { isSuccess = false; }
        
        public virtual void Get(out ITem itemData) { itemData = null; }
        
        public virtual void SetAlpha() { }

        public virtual void Reset() { }
    }
}