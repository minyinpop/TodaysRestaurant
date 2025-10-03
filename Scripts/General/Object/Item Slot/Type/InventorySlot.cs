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
        #endregion

        public override void Add(ItemSO item)
        {
            if (item is null) return;
            ItemData = item;
            ItemData.GetItemSprite(out var sprite);
            ItemImage.sprite = sprite;
            ItemImage.gameObject.SetActive(true);
        }

        public override void Get(ref ItemSO item)
        {
            if (ItemData is null) item = null;
            item = ItemData;
            ItemData = null;
            ItemImage.gameObject.SetActive(false);
            ItemImage.sprite = null;
        }

        public override bool IsEmpty()
        {
            return ItemData is null;
        }
    }
}