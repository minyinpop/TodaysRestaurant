using Common.Item.Data;
using Common.Storage_Slot.Main;

namespace Common.Storage_Slot.Child
{
    public sealed class HotbarSlot : StorageSlot
    {
        private bool IsSelected;
        
        #region Storage Slot
            #region PointerEvent
                protected override void OnPointerEnter()
                {
                    if (!interactable) return;
                    animation.DoScale_UI(slotRect, scaleUpSettings);
                }

                protected override void OnPointerExit()
                {
                    if (!interactable) return;
                    animation.DoScale_UI(slotRect, scaleDownSettings);
                }
            #endregion

            #region Interaction
                public override void Selected()
                {
                    slotImage.color = focusSlotColor;
                    _currentItem?.Selected();
                }

                public override void UnSelected()
                {
                    slotImage.color = normalSlotColor;
                    _currentItem?.UnSelected();
                }

                public override void Use()
                {
                    _currentItem?.Use();
                }
            #endregion

            #region Item
                public override bool TryAddItem(IItem item)
                {
                    if (item is null) return false;
                    if (_currentItem is not null) return false;
                    
                    _currentItem = item;
                    itemImage.sprite = item.ItemSprite;
                    itemImage.gameObject.SetActive(true);
                    return true;
                }

                public override void TryGetItem(out IItem itemData)
                {
                    if (_currentItem is null) itemData = null;
                    itemData = _currentItem;
                    _currentItem = null;
                    itemImage.gameObject.SetActive(false);
                    itemImage.sprite = null;
                }

                public override bool TryRemoveItem(IItem itemData)
                {
                    if (_currentItem is null) return false;
                    if (_currentItem != itemData) return false;
                    
                    itemImage.gameObject.SetActive(false);
                    itemImage.sprite = null;
                    
                    itemData.Remove();
                    _currentItem = null;
                    return true;
                }
            #endregion
        #endregion
    }
}