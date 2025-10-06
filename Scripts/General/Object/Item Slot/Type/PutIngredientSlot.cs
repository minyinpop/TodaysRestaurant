using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object.Item_Slot.Type
{
    internal sealed class PutIngredientSlot : ItemSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("State")]
        [field: SerializeField] private Color HaveItemColor;
        [field: SerializeField] private Color NoItemColor;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ScaleUpSettings;
        [field: SerializeField] private DoScale ScaleDownSettings;

        private ItemSO TargetItemData;
        private ItemSO ItemData;
        
        private bool Interactable;

        #region PointerEvent
        protected override void OnPointerEnter()
        {
            if (!Interactable) return;
            DoAnimation.DoScale(BackgroundRect, ScaleUpSettings);
        }

        protected override void OnPointerExit()
        {
            if (!Interactable) return;
            DoAnimation.DoScale(BackgroundRect, ScaleDownSettings);
        }
        #endregion

        public override bool Add(ItemSO item)
        {
            if (item is null) return false;
            if (ItemData is not null) return false;
            
            if (TargetItemData is null)
            {
                TargetItemData = item;
                TargetItemData.GetItemSprite(out var sprite);
                ItemImage.sprite = sprite;
            }
            else
            {
                item.GetItemType(out var type01, out var level01);
                TargetItemData.GetItemType(out var type02, out var level02);
                if (!Equals(type01, type02)) return false; // TODO 物品類型不同會跳出 Message System
                if (level01 < level02) return false; // TODO 物品類型相同但等級比 TargetItemData 還低，一樣跳出 Message System
                ItemData = item;
                ItemData.GetItemSprite(out var sprite);
                ItemImage.sprite = sprite;
                ItemImage.color = HaveItemColor;
            }

            return true;
        }

        public override void Get(out ItemSO item)
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

        public override void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            if (!interactable)
                DoAnimation?.DoScale(BackgroundRect, ScaleDownSettings);
        }
    }
}