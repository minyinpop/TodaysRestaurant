using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object.Item_Slot.Type
{
    internal sealed class SelectFoodSlot : ItemSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image DishImage;
        [field: SerializeField] private Text DishNameTMP;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ScaleUpSettings;
        [field: SerializeField] private DoScale ScaleDownSettings;

        private ItemSO ItemData;

        private bool Interactable;
        
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
            // TODO 清除資料
        }

        public override bool Add(ItemSO item)
        {
            if (item is null) return false;
            ItemData = item;
            ItemData.GetItemSprite(out var sprite);
            DishImage.sprite = sprite;
            DishImage.gameObject.SetActive(true);
            ItemData.GetItemName(out var itemName);
            DishNameTMP.text = itemName;
            DishNameTMP.gameObject.SetActive(true);
            return true;
        }
    }
}