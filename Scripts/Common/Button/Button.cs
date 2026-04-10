using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Audio_System.Data;
using Audio_System.Main;
using Common.Pointer_Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Button
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(DoAnimation))]
    public sealed class Button : PointerEvent
    {
        [field: Header("自身狀態設定")]
        [field: SerializeField] private bool Interactable;
        
        [field: Header("自身組件")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private TextMeshProUGUI TitleTMP;
        
        [field: Header("動畫設定")]
        [field: SerializeField] private DoAnimation DoAnimation;
        [field: SerializeField] private DoScale OnPointerEnterScale;
        [field: SerializeField] private DoScale OnPointerExitScale;
        
        [field: Header("聲音播放資料")]
        [field: SerializeField] private PlaySFXData playSFXData;

        public event Action OnClick;

        /* 2026.04.06 會跳 ERROR
        private void OnDisable()
        {
            if (Rect is not null)
            {
                OnPointerExitScale.GetValues(out var endValue, out _, out _);
                Rect.localScale = endValue;
            }
        }
        */

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
                if (Interactable)
                {
                    AudioSystem.Instance.SFXSystem.PlaySFX(playSFXData);
                    
                    DoAnimation?.DoScale_UI(Rect, OnPointerEnterScale);
                }
            }
            
            protected override void OnPointerExit()
            {
                if (Interactable)
                {
                    DoAnimation?.DoScale_UI(Rect, OnPointerExitScale);
                }
            }

            protected override void OnPointerClick()
            {
                if (!Interactable) return;
                if (OnClick == null)
                {
                    Debug.LogWarning($"{gameObject.name} > {GetType().Name} > {nameof(OnClick)} cannot be null.");
                    return;
                }

                OnClick.Invoke();
            }
        #endregion
    }
}