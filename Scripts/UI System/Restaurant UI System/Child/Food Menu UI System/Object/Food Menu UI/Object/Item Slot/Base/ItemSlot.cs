using System;
using Common.Item.Data;
using Common.Pointer_Event;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Base
{
    public abstract class ItemSlot : PointerEvent
    {
        protected bool Interactable;
        
        public event Action<ItemSlot, ItemSO> OnClick;
        protected void InvokeOnClick(ItemSO itemData)
        {
            OnClick?.Invoke(this, itemData);
        }

        protected override void OnPointerEnter() { }
        protected override void OnPointerExit() { }
        protected override void OnPointerClick() { }

        public virtual void SetSlotState(ItemSlotState slotState) { }
        public virtual void GetSlotState(out ItemSlotState slotState) { slotState = ItemSlotState.Lock; }
        
        public virtual void ChangeSelectState() { }

        public virtual void Add(ItemSO item) { }
        public virtual void Add(ItemSO item, out bool isSuccess) { isSuccess = false; }
        
        public virtual void Get(out ItemSO itemData) { itemData = null; }
        
        public virtual void SetAlpha() { }

        public virtual void Reset() { }

        public abstract void SetInteractable(bool interactable);
    }
}