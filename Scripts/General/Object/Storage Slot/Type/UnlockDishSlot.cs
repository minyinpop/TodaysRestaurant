using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.UI;

namespace General.Object.Storage_Slot.Type
{
    internal sealed class UnlockDishSlot : StorageSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ScaleUpSettings;
        [field: SerializeField] private DoScale ScaleDownSettings;

        private ItemSO ItemData;

        private bool Interactable = true;
        
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
    }
}