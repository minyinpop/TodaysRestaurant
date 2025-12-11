using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Object.Storage_Slot.Base;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Object.Storage_Slot.Type
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
        
        private ITem ItemData;

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

        public override void TryAddItem(ITem item, out bool isSuccess)
        {
            if (item is null) { isSuccess = false; return; }
            if (ItemData is not null) { isSuccess = false; return; }
            
            ItemData = item;
            ItemData.GetItemSprite(out var sprite);
            ItemImage.sprite = sprite;
            ItemImage.gameObject.SetActive(true);
            isSuccess = true;
        }

        public override void GetItem(out ITem item)
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