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
    }
}