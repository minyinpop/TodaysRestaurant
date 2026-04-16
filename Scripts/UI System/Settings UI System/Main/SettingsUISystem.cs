using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Button;
using UI_System.Settings_UI_System.Child;
using UnityEngine;

namespace UI_System.Settings_UI_System.Main
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class SettingsUISystem : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("自身系統")]
        [field: SerializeField] private AudioSettingsUISystem audioSettingsUISystem;
        
        [field: Header("遮罩")]
        [field: SerializeField] private CanvasGroup maskCanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeOutSettings;
        
        [field: Header("介面")]
        [field: SerializeField] private CanvasGroup settingsUICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup settingsUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup settingsUIFadeOutSettings;

        [field: Header("按鈕")]
        [field: SerializeField] private Button closeButton;
        
        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }

            if (audioSettingsUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(audioSettingsUISystem)} 沒有被掛載。");
            }

            if (maskCanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(maskCanvasGroup)} 沒有被掛載。");
            }

            if (settingsUICanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(settingsUICanvasGroup)} 沒有被掛載。");
            }

            if (closeButton is null)
            {
                throw new InvalidOperationException($"{nameof(closeButton)} 沒有被掛載。");
            }
            
            closeButton.OnClick += OnClickCloseButton;
        }

        private void Start()
        {
            audioSettingsUISystem.Initialize();
        }

        private void OnDestroy()
        {
            closeButton.OnClick -= OnClickCloseButton;
        }

        public void OpenUI()
        {
            maskCanvasGroup.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: maskCanvasGroup,
                settings: maskFadeInSettings,
                onComplete: () =>
                {
                    settingsUICanvasGroup.gameObject.SetActive(true);
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: settingsUICanvasGroup,
                        settings: settingsUIFadeInSettings);
                });
        }

        private void OnClickCloseButton()
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: settingsUICanvasGroup,
                settings: settingsUIFadeOutSettings,
                onComplete: () =>
                {
                    settingsUICanvasGroup.gameObject.SetActive(false);
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: maskCanvasGroup,
                        settings: maskFadeOutSettings,
                        onComplete: () =>
                        {
                            maskCanvasGroup.gameObject.SetActive(false);
                        });
                });
        }
    }
}