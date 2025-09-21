using System;
using System.General.DOTween;
using Data.Animation.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace General.Object
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class Button : PointerEvent
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;

        private bool Interactable = false;

        public event Action OnClick;
        
        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;
        }
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                DoAnimation.DoScale(Rect, new DoScale(Vector2.one * 1.1f, .15f, Ease.OutQuart));
            }
            
            protected override void OnPointerExit()
            {
                DoAnimation.DoScale(Rect, new DoScale(Vector2.one, .15f, Ease.OutQuart));
            }

            protected override void OnPointerClick()
            {
                if (Interactable)
                    OnClick?.Invoke();
            }
        #endregion
    }
}