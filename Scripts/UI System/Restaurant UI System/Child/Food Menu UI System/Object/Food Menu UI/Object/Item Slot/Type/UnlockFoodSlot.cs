using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Data.Item;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Type
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

        private ItemSlotState SlotState = ItemSlotState.Lock;
        
        protected override void OnPointerEnter()
        {
            if (SlotState == ItemSlotState.Lock) return;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleUpSettings);
        }

        protected override void OnPointerExit()
        {
            if (SlotState == ItemSlotState.Lock) return;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleDownSettings);
        }

        protected override void OnPointerClick()
        {
            if (SlotState == ItemSlotState.Lock) return;
            OnClicked(ItemData);
        }

        public override void SetSlotState(ItemSlotState slotState)
        {
            SlotState = slotState;
        }
        
        public override void GetSlotState(out ItemSlotState slotState)
        {
            slotState = SlotState;
        }

        public override void ChangeSelectState()
        {
            SlotState = SlotState switch
            {
                ItemSlotState.Select => ItemSlotState.UnSelect,
                ItemSlotState.UnSelect => ItemSlotState.Select,
                _ => SlotState
            };
        }
        
        public override void Add(ItemSO itemData)
        {
            if (itemData is null) return;
            ItemData = itemData;
            ItemImage.sprite = ItemData.ItemSprite;
            ItemImage.gameObject.SetActive(true);
        }

        public override void Get(out ItemSO itemData)
        {
            itemData = ItemData;
        }

        public override void SetAlpha()
        {
            foreach (var image in AllImage)
            {
                image.color = SlotState switch
                {
                    ItemSlotState.Select => new Color(image.color.r, image.color.g, image.color.b, OnSelectAlpha),
                    ItemSlotState.UnSelect => new Color(image.color.r, image.color.g, image.color.b, UnSelectAlpha),
                    _ => image.color
                };
            }
        }
    }
}