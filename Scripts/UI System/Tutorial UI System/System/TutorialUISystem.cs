using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Button;
using UnityEngine;

namespace UI_System.Tutorial_UI_System.System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class TutorialUISystem : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("遮罩")]
        [field: SerializeField] private CanvasGroup maskCanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeOutSettings;
        
        [field: Header("介面")]
        [field: SerializeField] private CanvasGroup tutorialUICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup tutorialUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup tutorialUIFadeOutSettings;
        
        [field: Header("按鈕")]
        [field: SerializeField] private Button closeButton;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
            
            if (maskCanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(maskCanvasGroup)} 沒有被掛載。");
            }
            
            if (tutorialUICanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(tutorialUICanvasGroup)} 沒有被掛載。");
            }
            
            if (closeButton is null)
            {
                throw new InvalidOperationException($"{nameof(closeButton)} 沒有被掛載。");
            }
            
            closeButton.OnClick += CloseUI;
        }
        
        private void OnDestroy()
        {
            closeButton.OnClick -= CloseUI;
        }

        public void OpenUI()
        {
            maskCanvasGroup.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: maskCanvasGroup,
                settings: maskFadeInSettings,
                onComplete: () =>
                {
                    tutorialUICanvasGroup.gameObject.SetActive(true);

                    animation.DoFade_CanvasGroup(
                        canvasGroup: tutorialUICanvasGroup,
                        settings: tutorialUIFadeInSettings);
                });
        }

        private void CloseUI()
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: tutorialUICanvasGroup,
                settings: tutorialUIFadeOutSettings,
                onComplete: () =>
                {
                    tutorialUICanvasGroup.gameObject.SetActive(false);
                    
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