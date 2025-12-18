using System;
using Item.Data;
using UnityEngine;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class StorageGetSlot : StorageSlot
    {
        private ItemSO ItemData;
        
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
        
        public override void TryAddItem(ItemSO item, Action onComplete)
        {
            if (item is null) return;
            ItemData = item;
            ItemImage.sprite = item.ItemSprite;
            ItemImage.gameObject.SetActive(true);
            SlotRect.localScale = Vector2.one * 1.25f;
            DoAnimation.DoScale_UI(SlotRect, ScaleDownSettings,
                onComplete: () =>
                {
                    Interactable = true;
                    onComplete?.Invoke();
                });
        }
    }
}