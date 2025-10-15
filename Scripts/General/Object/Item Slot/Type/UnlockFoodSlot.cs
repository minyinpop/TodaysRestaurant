using System.Collections.Generic;
using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object.Item_Slot.Type
{
    internal sealed class UnlockFoodSlot : ItemSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Alpha")]
        [field: SerializeField] private List<Image> AllImage;
        [field: SerializeField, Range(0, 1)] private float OnSelectAlpha;
        [field: SerializeField, Range(0, 1)] private float UnSelectAlpha;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ScaleUpSettings;
        [field: SerializeField] private DoScale ScaleDownSettings;

        private ItemSO ItemData;

        private bool Interactable;
        private bool OnSelect;
        
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

        protected override void OnPointerClick()
        {
            if (!Interactable) return;
            OnSelect = !OnSelect;
            OnClicked(OnSelect, ItemData);
        }

        public override bool Add(ItemSO itemData)
        {
            if (ItemData is not null) return false;
            ItemData = itemData;
            ItemData.GetItemSprite(out var sprite);
            ItemImage.sprite = sprite;
            ItemImage.gameObject.SetActive(true);
            return true;
        }

        public override void SetInteractable(bool interactable)
        {
            Interactable = interactable;
        }

        public override void SetAlpha()
        {
            foreach (var image in AllImage) image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                OnSelect ? OnSelectAlpha : UnSelectAlpha);
        }
    }
}