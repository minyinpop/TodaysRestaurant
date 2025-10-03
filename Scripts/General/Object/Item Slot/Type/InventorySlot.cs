using System;
using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using DG.Tweening;
using General.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object.Item_Slot.Type
{
    internal sealed class InventorySlot : ItemSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("State")]
        [field: SerializeField] private bool Interactable;

        [field: Header("Develop Only")]
        [field: SerializeField] private ItemSO ItemData;
        
        public static event Func<ItemSO, bool> OnClicked;
        
        #region PointerEvent
        protected override void OnPointerEnter()
        {
            if (!Interactable) return;
            DoAnimation.DoScale(BackgroundRect, new DoScale(Vector2.one * 1.2f, .2f, Ease.OutExpo));
        }

        protected override void OnPointerExit()
        {
            if (!Interactable) return;
            DoAnimation.DoScale(BackgroundRect, new DoScale(Vector2.one, .2f, Ease.OutExpo));
        }

        protected override void OnPointerClick()
        {
            if (!Interactable) return;
            if (OnClicked?.Invoke(ItemData) is true)
            {
            }
            else
            {
            }
        }
        #endregion

        public override bool Add(ItemSO item, Action onComplete)
        {
            if (item is null) return false;
            ItemData = item;
            ItemData.GetItemSprite(out var sprite);
            ItemImage.sprite = sprite;
            BackgroundRect.localScale = Vector2.one * 1.25f;
            DoAnimation.DoScale(BackgroundRect, new DoScale(Vector2.one, .5f, Ease.OutBounce),
                onComplete: () =>
                {
                    Interactable = true;
                    onComplete?.Invoke();
                });
            return true;
        }
    }
}