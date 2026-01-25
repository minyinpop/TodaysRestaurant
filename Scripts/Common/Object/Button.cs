using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Object
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(DoAnimation))]
    public sealed class Button : PointerEvent
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private TextMeshProUGUI TitleTMP;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoAnimation DoAnimation;
        [field: SerializeField] private DoScale OnPointerEnterScale;
        [field: SerializeField] private DoScale OnPointerExitScale;
        
        [field: Header("Status Settings")]
        [field: SerializeField] private bool Interactable;

        public event Action OnClicked;

        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            if (!interactable) DoAnimation?.DoScale_UI(Rect, OnPointerExitScale);
        }

        public void SetTitle(string title)
        {
            TitleTMP?.SetText(title);
        }
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (Interactable) DoAnimation?.DoScale_UI(Rect, OnPointerEnterScale);
            }
            
            protected override void OnPointerExit()
            {
                if (Interactable) DoAnimation?.DoScale_UI(Rect, OnPointerExitScale);
            }

            protected override void OnPointerClick()
            {
                if (!Interactable) return;
                if (OnClicked == null)
                {
                    Debug.LogWarning($"{gameObject.name} > Button > OnClicked cannot be null.");
                    gameObject.SetActive(false);
                    return;
                }

                OnClicked.Invoke();
            }
        #endregion
    }
}