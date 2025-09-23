using System;
using System.General.DOTween;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class ItemSlot : PointerEvent
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        private ITem Item;

        private bool Interactable;
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (Interactable)
                    DoAnimation.DoScale(Rect, new DoScale(Vector2.one * 1.2f, .2f, Ease.OutExpo));
            }

            protected override void OnPointerExit()
            {
                if (Interactable)
                    DoAnimation.DoScale(Rect, new DoScale(Vector2.one, .2f, Ease.OutExpo));
            }
        #endregion

        public void Add(ITem item, Action onComplete)
        {
            Item = item;
            Item.GetInformationSettings(out var sprite);
            ItemImage.sprite = sprite;
            Rect.localScale = Vector2.one * 1.25f;
            DoAnimation.DoScale(Rect, new DoScale(Vector2.one, .5f, Ease.OutBounce),
                onComplete: () =>
                {
                    Interactable = true;
                    onComplete?.Invoke();
                });
        }
    }
}