using Item;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class HotbarSlot : StorageSlot
    {
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
                    _currentItemData?.Selected();
                }

                public override void UnSelected()
                {
                    SlotImage.color = UnSelectedColor;
                    _currentItemData?.UnSelected();
                }

                public override void Use()
                {
                    _currentItemData?.Use();
                }
            #endregion

            #region Item
                public override bool TryAddItem(ItemSO item)
                {
                    if (item is null) return false;
                    if (_currentItemData is not null) return false;
                    
                    _currentItemData = item;
                    ItemImage.sprite = item.ItemSprite;
                    ItemImage.gameObject.SetActive(true);
                    return true;
                }

                public override void TryGetItem(out ItemSO item)
                {
                    if (_currentItemData is null) item = null;
                    item = _currentItemData;
                    _currentItemData = null;
                    ItemImage.gameObject.SetActive(false);
                    ItemImage.sprite = null;
                }
            #endregion
        #endregion
    }
}