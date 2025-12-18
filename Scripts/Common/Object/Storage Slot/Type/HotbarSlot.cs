using Item.Data;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class HotbarSlot : StorageSlot
    {
        private ItemSO ItemData;
        private bool IsSelected;
        
        #region Storage Slot
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

            #region Interaction
                public override void Selected()
                {
                    SlotImage.color = SelectedColor;
                    ItemData?.Selected();
                }

                public override void UnSelected()
                {
                    SlotImage.color = UnSelectedColor;
                    ItemData?.UnSelected();
                }

                public override void Use()
                {
                    ItemData?.Use();
                }
            #endregion

            #region Item
                public override void TryAddItem(ItemSO item, out bool isSuccess)
                {
                    if (item is null) { isSuccess = false; return; }
                    if (ItemData is not null) { isSuccess = false; return; }
                    
                    ItemData = item;
                    ItemImage.sprite = item.ItemSprite;
                    ItemImage.gameObject.SetActive(true);
                    isSuccess = true;
                }

                public override void GetItem(out ItemSO item)
                {
                    if (ItemData is null) item = null;
                    item = ItemData;
                    ItemData = null;
                    ItemImage.gameObject.SetActive(false);
                    ItemImage.sprite = null;
                }
            #endregion

            #region Status
                public override bool IsEmpty()
                {
                    return ItemData is null;
                }
            #endregion
        #endregion
    }
}