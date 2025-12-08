using Data.Item.Abstract;
using Tool;

namespace System.Economy.Child.Food_Menu.Object.Item_Slot.Base
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

        public virtual void Add(ItemSO item) { }
        public virtual void Add(ItemSO item, out bool isSuccess) { isSuccess = false; }
        
        public virtual void Get(out ItemSO itemData) { itemData = null; }
        
        public virtual void SetAlpha() { }

        public virtual void Reset() { }
    }
}