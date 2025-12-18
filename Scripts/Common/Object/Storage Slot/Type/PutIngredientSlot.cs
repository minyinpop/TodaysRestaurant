using Item.Data;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class PutIngredientSlot : StorageSlot
    {
        private ItemSO TargetItemData;
        private ItemSO ItemData;
        
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

        public override void TryAddItem(ItemSO item, out bool isSuccess)
        {
            if (item is null) { isSuccess = false; return; }
            if (ItemData is not null) { isSuccess = false; return; }
            
            if (TargetItemData is null)
            {
                TargetItemData = item;
                ItemImage.sprite = item.ItemSprite;
            }
            else
            {
                item.GetItemType(out var type01, out var level01);
                TargetItemData.GetItemType(out var type02, out var level02);
                if (!Equals(type01, type02)) { isSuccess = false; return; } // TODO 物品類型不同會跳出 Message System
                if (level01 < level02) { isSuccess = false; return; } // TODO 物品類型相同但等級比 TargetItemData 還低，一樣跳出 Message System
                ItemData = item;
                ItemImage.sprite = item.ItemSprite;
                ItemImage.color = HaveItemColor;
            }

            isSuccess = true;
        }

        public override void GetItem(out ItemSO item)
        {
            if (ItemData is null)
            {
                item = null;
                return;
            }

            item = ItemData;
            ItemData = null;
            ItemImage.sprite = item.ItemSprite;
            ItemImage.color = NoItemColor;
        }
        
        public override bool IsEmpty()
        {
            return ItemData is null;
        }

        public override void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            if (!interactable)
                DoAnimation?.DoScale_UI(SlotRect, ScaleDownSettings);
        }
    }
}