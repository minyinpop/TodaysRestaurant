using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Button;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.Object
{
    [RequireComponent(typeof(Button))]
    public sealed class OpenClosedUI : MonoBehaviour
    {
        [field: Header("動畫")]
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private DoScale scale1Settings;
        [field: SerializeField] private DoScale scale2Settings;
        
        [field: Header("按鈕")]
        [field: SerializeField] private Button button;
        
        [field: Header("物件")]
        [field: SerializeField] private RectTransform rect;
        [field: SerializeField] private RectTransform front;
        [field: SerializeField] private RectTransform back;

        private bool _isOpen;

        public Action OnOpen;
        public Action OnClosed;

        private void Awake()
        {
            button.OnClick += OnClickButton;
        }

        private void OnDestroy()
        {
            button.OnClick -= OnClickButton;
        }

        private void OnClickButton()
        {
            SetInteractable(false);
            
            _isOpen = !_isOpen;

            animation.DoScale_UI(
                rect: rect,
                settings: scale1Settings,
                onComplete: () =>
                {
                    if (_isOpen)
                    {
                        front.gameObject.SetActive(true);
                        back.gameObject.SetActive(false);
                        
                        OnOpen.Invoke();
                    }
                    else
                    {
                        front.gameObject.SetActive(false);
                        back.gameObject.SetActive(true);
                        
                        OnClosed.Invoke();
                    }
                    
                    animation.DoScale_UI(
                        rect: rect,
                        settings: scale2Settings,
                        onComplete: () =>
                        {
                            SetInteractable(true);
                        });
                });
        }

        public void SetInteractable(bool interactable)
        {
            button.SetInteractable(interactable);
        }
    }
}