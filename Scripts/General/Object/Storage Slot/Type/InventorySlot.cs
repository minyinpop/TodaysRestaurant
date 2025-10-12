using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using General.Object.Storage_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object.Storage_Slot.Type
{
    internal sealed class InventorySlot : StorageSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ScaleUpSettings;
        [field: SerializeField] private DoScale ScaleDownSettings;
        
        [field: Header("Develop Only")]
        [field: SerializeField] private ItemSO ItemData;

        private bool Interactable = true;
        
        #region PointerEvent
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
        #endregion

        public override bool Add(ItemSO item)
        {
            if (item is null) return false;
            ItemData = item;
            ItemData.GetItemSprite(out var sprite);
            ItemImage.sprite = sprite;
            ItemImage.gameObject.SetActive(true);
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
    }
}