using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Object.Storage_Slot.Base;
using Item;
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

        private ITem ItemData;

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
        
        public override void TryAddItem(ITem item, Action onComplete)
        {
            if (item is null) return;
            ItemData = item;
            ItemData.GetItemSprite(out var sprite);
            ItemImage.sprite = sprite;
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