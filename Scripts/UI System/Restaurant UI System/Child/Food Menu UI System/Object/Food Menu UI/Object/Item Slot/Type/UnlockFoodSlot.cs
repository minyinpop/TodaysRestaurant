using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item.Data;
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

        private ItemSlotState _slotState = ItemSlotState.Lock;
        public ItemSlotState SlotState => _slotState;
        
        protected override void OnPointerEnter()
        {
            if (_slotState == ItemSlotState.Lock) return;
            if (!Interactable) return;
            
            DoAnimation.DoScale_UI(BackgroundRect, ScaleUpSettings);
        }

        protected override void OnPointerExit()
        {
            if (_slotState == ItemSlotState.Lock) return;
            if (!Interactable) return;
            
            DoAnimation.DoScale_UI(BackgroundRect, ScaleDownSettings);
        }

        protected override void OnPointerClick()
        {
            if (_slotState == ItemSlotState.Lock) return;
            if (!Interactable) return;
            
            InvokeOnClick(ItemData);
        }

        public override void SetSlotState(ItemSlotState slotState)
        {
            _slotState = slotState;
        }
        
        public override void GetSlotState(out ItemSlotState slotState)
        {
            slotState = _slotState;
        }

        public override void ChangeSelectState()
        {
            _slotState = _slotState switch
            {
                ItemSlotState.Select => ItemSlotState.UnSelect,
                ItemSlotState.UnSelect => ItemSlotState.Select,
                _ => _slotState
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
                image.color = _slotState switch
                {
                    ItemSlotState.Select => new Color(image.color.r, image.color.g, image.color.b, OnSelectAlpha),
                    ItemSlotState.UnSelect => new Color(image.color.r, image.color.g, image.color.b, UnSelectAlpha),
                    _ => image.color
                };
            }
        }

        public override void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleDownSettings);
        }
    }
}