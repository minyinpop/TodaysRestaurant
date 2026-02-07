using Common.Data.Item;
using Common.Object.Storage_Slot.Main;

namespace Common.Object.Storage_Slot.Child
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
                public override bool TryAddItem(ItemSO itemData)
                {
                    if (itemData is null) return false;
                    if (_currentItemData is not null) return false;
                    
                    _currentItemData = itemData;
                    ItemImage.sprite = itemData.ItemSprite;
                    ItemImage.gameObject.SetActive(true);
                    return true;
                }

                public override void TryGetItem(out ItemSO itemData)
                {
                    if (_currentItemData is null) itemData = null;
                    itemData = _currentItemData;
                    _currentItemData = null;
                    ItemImage.gameObject.SetActive(false);
                    ItemImage.sprite = null;
                }

                public override bool TryRemoveItem(ItemSO itemData)
                {
                    if (_currentItemData is null) return false;
                    if (_currentItemData != itemData) return false;
                    
                    ItemImage.gameObject.SetActive(false);
                    ItemImage.sprite = null;
                    
                    itemData.Remove();
                    _currentItemData = null;
                    return true;
                }
            #endregion
        #endregion
    }
}