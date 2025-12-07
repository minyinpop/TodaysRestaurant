using System;
using Data.Animation.DOTween.Basic;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.UI;

namespace Object
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class Button : PointerEvent
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Object")]
        [field: SerializeField] private TextMeshProUGUI TitleTMP;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale OnPointerEnterScale;
        [field: SerializeField] private DoScale OnPointerExitScale;
        
        private bool Interactable;

        public event Action OnClick;
        
        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            if (!interactable)
                DoAnimation?.DoScale_UI(Rect, OnPointerExitScale);
        }

        public void SetTitle(string title)
        {
            TitleTMP?.SetText(title);
        }
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (Interactable)
                    DoAnimation?.DoScale_UI(Rect, OnPointerEnterScale);
            }
            
            protected override void OnPointerExit()
            {
                if (Interactable)
                    DoAnimation?.DoScale_UI(Rect, OnPointerExitScale);
            }

            protected override void OnPointerClick()
            {
                if (Interactable)
                    OnClick?.Invoke();
            }
        #endregion
    }
}