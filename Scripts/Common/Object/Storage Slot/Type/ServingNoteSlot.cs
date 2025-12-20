using Item;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class ServingNoteSlot : StorageSlot
    {
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!Interactable) return;
                DoAnimation.DoScale_UI(SlotRect, ScaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!Interactable) return;
                DoAnimation.DoScale_UI(SlotRect, ScaleDownSettings);
            }
        #endregion
        
        #region Item
            public override bool TryAddItem(ItemSO item)
            {
                if (item is null) return false;
                if (ItemData is not null) return false;
                
                ItemData = item;
                ItemImage.sprite = item.ItemSprite;
                ItemImage.gameObject.SetActive(true);
                return true;
            }

            public override void TryGetItem(out ItemSO item)
            {
                if (ItemData is null) item = null;
                item = ItemData;
                ItemData = null;
                ItemImage.gameObject.SetActive(false);
                ItemImage.sprite = null;
            }
        #endregion
    }
}