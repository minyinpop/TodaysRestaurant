using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Item.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Object.Storage_Slot.Type
{
    internal sealed class StorageGetSlot : StorageSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ScaleUpSettings;
        [field: SerializeField] private DoScale ScaleDownSettings;

        private ItemSO ItemData;

        private bool Interactable = true;
        
        protected override void OnPointerEnter()
        {
            if (!Interactable) return;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleUpSettings);
        }

        protected override void OnPointerExit()
        {
            if (!Interactable) return;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleDownSettings);
        }
        
        public override void TryAddItem(ItemSO item, Action onComplete)
        {
            if (item is null) return;
            ItemData = item;
            ItemImage.sprite = item.ItemSprite;
            ItemImage.gameObject.SetActive(true);
            BackgroundRect.localScale = Vector2.one * 1.25f;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleDownSettings,
                onComplete: () =>
                {
                    Interactable = true;
                    onComplete?.Invoke();
                });
        }
    }
}