using System;
using System.General.DOTween;
using Data.Animation.DOTween.Basic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace General.Object
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class Button : PointerEvent
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Object")]
        [field: SerializeField] private TextMeshProUGUI TitleTMP;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;

        private bool Interactable;

        public event Action OnClick;
        
        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;

            if (!interactable)
                DoAnimation.DoScale(Rect, new DoScale(Vector2.one, .15f, Ease.OutExpo));
        }

        public void SetTitle(string title)
        {
            TitleTMP.text = title;
        }
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (Interactable)
                    DoAnimation.DoScale(Rect, new DoScale(Vector2.one * 1.1f, .15f, Ease.OutQuart));
            }
            
            protected override void OnPointerExit()
            {
                if (Interactable)
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