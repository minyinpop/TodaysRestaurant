using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Economy_System.Child.Food_Menu_System.Object.Item_Slot.Base;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Economy_System.Child.Food_Menu_System.Object.Item_Slot.Type.Select_Food_Slot
{
    internal sealed class SelectFoodSlot : ItemSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image BackgroundImage;
        [field: SerializeField] private Image FoodImage;
        [field: SerializeField] private TextMeshProUGUI FoodNameTMP;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Slot Sprite")]
        [field: SerializeField] private Sprite LockSprite;
        [field: SerializeField] private Sprite NoItemSprite;
        [field: SerializeField] private Sprite HaveItemSprite;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ScaleUpSettings;
        [field: SerializeField] private DoScale ScaleDownSettings;

        private ITem ItemData;
        
        private ItemSlotState SlotState = ItemSlotState.Lock;
        
        protected override void OnPointerEnter()
        {
            if (SlotState != ItemSlotState.HaveItem) return;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleUpSettings);
        }

        protected override void OnPointerExit()
        {
            if (SlotState != ItemSlotState.HaveItem) return;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleDownSettings);
        }

        protected override void OnPointerClick()
        {
            if (SlotState != ItemSlotState.HaveItem) return;
            DoAnimation.DoScale_UI(BackgroundRect, ScaleDownSettings);
            OnClicked(ItemData);
        }

        #region Item Slot State
            public override void SetSlotState(ItemSlotState slotState)
            {
                SlotState = slotState;
                switch (SlotState)
                {
                    case ItemSlotState.Lock:
                    {
                        LockState();
                        break;
                    }
                    case ItemSlotState.NoItem:
                    {
                        NoItemState();
                        break;
                    }
                    case ItemSlotState.HaveItem:
                    {
                        HaveItemState();
                        break;
                    }
                }

                return;

                void LockState()
                {
                    BackgroundImage.sprite = LockSprite;
                    SlotState = ItemSlotState.Lock;
                    ItemData = null;
                    HideFoodInfo();
                }

                void NoItemState()
                {
                    BackgroundImage.sprite = NoItemSprite;
                    SlotState = ItemSlotState.NoItem;
                    ItemData = null;
                    HideFoodInfo();
                }
                
                void HaveItemState()
                {
                    BackgroundImage.sprite = HaveItemSprite;
                    SlotState = ItemSlotState.HaveItem;
                }
            }
            
            public override void GetSlotState(out ItemSlotState slotState)
            {
                slotState = SlotState;
            }
        #endregion

        public override void Add(ITem item)
        {
            if (SlotState is ItemSlotState.Lock or ItemSlotState.HaveItem) return;
            SlotState = ItemSlotState.HaveItem;
            ItemData = item;
            BackgroundImage.sprite = HaveItemSprite;
            ShowFoodInfo();
        }

        public override void Add(ITem item, out bool isSuccess)
        {
            if (SlotState is ItemSlotState.Lock or ItemSlotState.HaveItem)
            {
                isSuccess = false;
                return;
            }

            SlotState = ItemSlotState.HaveItem;
            ItemData = item;
            BackgroundImage.sprite = HaveItemSprite;
            ShowFoodInfo();
            isSuccess = true;
        }

        public override void Get(out ITem itemData)
        {
            itemData = ItemData;
        }

        public override void Reset()
        {
            if (SlotState is not ItemSlotState.HaveItem) return;
            SlotState = ItemSlotState.NoItem;
            ItemData = null;
            BackgroundImage.sprite = NoItemSprite;
            HideFoodInfo();
        }

        #region Info
            private void ShowFoodInfo()
            {
                ItemData.GetItemSprite(out var sprite);
                FoodImage.sprite = sprite;
                FoodImage.gameObject.SetActive(true);
                ItemData.GetItemName(out var itemName);
                FoodNameTMP.text = itemName;
                FoodNameTMP.gameObject.SetActive(true);
            }

            private void HideFoodInfo()
            {
                FoodImage.gameObject.SetActive(false);
                FoodImage.sprite = null;
                FoodNameTMP.gameObject.SetActive(false);
                FoodNameTMP.text = string.Empty;
            }
        #endregion
    }
}