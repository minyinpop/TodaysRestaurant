using Common.Item.Data;
using Common.Storage_Slot.Main;

namespace Common.Storage_Slot.Child
{
    public sealed class ServingNoteSlot : StorageSlot
    {
        private ItemSO _targetItemData;

        public void Initialize(ItemSO targetItemData)
        {
            if (_targetItemData is not null) return;
            _targetItemData = targetItemData;
            itemImage.sprite = targetItemData.ItemSprite;
        }

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
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
        
        #region Item
            public override bool TryAddItem(IItem item)
            {
                if (item is null) return false;
                if (_currentItem is not null) return false;
                if (item.ItemID != _targetItemData.ItemID) return false;
                
                _currentItem = item;
                itemImage.sprite = item.ItemSprite;
                itemImage.color = haveItemColor;
                return true;
            }

            public override void TryGetItem(out IItem itemData)
            {
                if (_currentItem is null)
                {
                    itemData = null;
                    return;
                }
                
                itemData = _currentItem;
                _currentItem = null;
                itemImage.sprite = _targetItemData.ItemSprite;
                itemImage.color = noItemColor;
            }

            public override void TryPeekItem(out IItem itemData)
            {
                itemData = _currentItem;
            }
        #endregion

        public override bool IsEmpty()
        {
            return _currentItem is null;
        }
    }
}