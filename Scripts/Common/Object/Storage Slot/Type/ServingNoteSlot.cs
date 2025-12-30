using Item;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class ServingNoteSlot : StorageSlot
    {
        private ItemSO _targetItemData;

        public void Initialize(ItemSO targetItemData)
        {
            if (_targetItemData is not null) return;
            _targetItemData = targetItemData;
            ItemImage.sprite = targetItemData.ItemSprite;
        }

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
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
        
        #region Item
            public override bool TryAddItem(ItemSO itemData)
            {
                if (itemData is null) return false;
                if (_currentItemData is not null) return false;
                if (itemData.ItemID != _targetItemData.ItemID) return false;
                
                _currentItemData = itemData;
                ItemImage.sprite = itemData.ItemSprite;
                ItemImage.color = HaveItemColor;
                return true;
            }

            public override void TryGetItem(out ItemSO itemData)
            {
                if (_currentItemData is null)
                {
                    itemData = null;
                    return;
                }
                
                itemData = _currentItemData;
                _currentItemData = null;
                ItemImage.sprite = _targetItemData.ItemSprite;
                ItemImage.color = NoItemColor;
            }

            public override void TryPeekItem(out ItemSO itemData)
            {
                itemData = _currentItemData;
            }
        #endregion

        public override bool IsEmpty()
        {
            return _currentItemData is null;
        }
    }
}