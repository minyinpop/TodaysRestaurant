using Item;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class TrashBinSlot : StorageSlot
    {
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!Interactable) return;
                SlotImage.sprite = PointerEnterSprite;
                DoAnimation.DoScale_UI(SlotRect, ScaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!Interactable) return;
                SlotImage.sprite = PointerExitSprite;
                DoAnimation.DoScale_UI(SlotRect, ScaleDownSettings);
            }
        #endregion
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
        
        #region Item
            public override bool TryAddItem(ItemSO itemData)
            {
                return itemData.ItemID != 999;
            }
        #endregion
    }
}