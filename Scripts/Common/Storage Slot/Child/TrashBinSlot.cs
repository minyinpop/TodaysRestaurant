using Common.Item.Data;
using Common.Storage_Slot.Main;

namespace Common.Storage_Slot.Child
{
    public sealed class TrashBinSlot : StorageSlot
    {
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!interactable) return;
                slotImage.sprite = pointerEnterSprite;
                animation.DoScale_UI(slotRect, scaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!interactable) return;
                slotImage.sprite = pointerExitSprite;
                animation.DoScale_UI(slotRect, scaleDownSettings);
            }
        #endregion
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
        
        #region Item
            public override bool TryAddItem(IItem item)
            {
                return item.ItemID != 999;
            }
        #endregion
    }
}