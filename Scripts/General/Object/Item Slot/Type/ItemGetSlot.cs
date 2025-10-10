using System;
using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object.Item_Slot.Type
{
    internal sealed class ItemGetSlot : ItemSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale scaleUpSettings;
        [field: SerializeField] private DoScale scaleDownSettings;

        private ItemSO ItemData;

        private bool Interactable = true;
        
        protected override void OnPointerEnter()
        {
            if (!Interactable) return;
            DoAnimation.DoScale_UI(BackgroundRect, scaleUpSettings);
        }

        protected override void OnPointerExit()
        {
            if (!Interactable) return;
            DoAnimation.DoScale_UI(BackgroundRect, scaleDownSettings);
        }
        
        public override void Add(ItemSO item, Action onComplete)
        {
            if (item is null) return;
            ItemData = item;
            ItemData.GetItemSprite(out var sprite);
            ItemImage.sprite = sprite;
            ItemImage.gameObject.SetActive(true);
            BackgroundRect.localScale = Vector2.one * 1.25f;
            DoAnimation.DoScale_UI(BackgroundRect, scaleDownSettings,
                onComplete: () =>
                {
                    Interactable = true;
                    onComplete?.Invoke();
                });
        }
    }
}