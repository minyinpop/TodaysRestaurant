using Item;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class PutIngredientSlot : StorageSlot
    {
        private ItemSO _targetItemData;
        
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
            public override bool TryAddItem(ItemSO itemData)
            {
                if (itemData is null) return false;
                if (_currentItemData is not null) return false;
                
                if (_targetItemData is null)
                {
                    _targetItemData = itemData;
                    ItemImage.sprite = itemData.ItemSprite;
                }
                else
                {
                    itemData.GetItemType(out var type01, out var level01);
                    _targetItemData.GetItemType(out var type02, out var level02);
                    if (!Equals(type01, type02)) return false; // TODO 物品類型不同會跳出 Message System
                    if (level01 < level02) return false; // TODO 物品類型相同但等級比 TargetItemData 還低，一樣跳出 Message System
                    _currentItemData = itemData;
                    ItemImage.sprite = itemData.ItemSprite;
                    ItemImage.color = HaveItemColor;
                }

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
        #endregion
    }
}