using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using UnityEngine;

namespace UI_System.Title_UI_System.Child.Login_UI_System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class LoginUISystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("mask")]
        [field: SerializeField] private CanvasGroup maskCanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeOutSettings;
        
        [field: Header("UI")]
        [field: SerializeField] private LoginUI loginUI;
        [field: SerializeField] private CanvasGroup loginUICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup loginUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup loginUIFadeOutSettings;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
            }

            if (maskCanvasGroup is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(maskCanvasGroup)} cannot be null.");
            }

            if (loginUI is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(loginUI)} cannot be null.");
            }
            
            if (loginUICanvasGroup is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(loginUICanvasGroup)} cannot be null.");
            }
            
            maskCanvasGroup.gameObject.SetActive(false);
            loginUICanvasGroup.gameObject.SetActive(false);
        }

        public void OpenLoginUI(Action onLogin)
        {
            maskCanvasGroup.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: maskCanvasGroup,
                settings: maskFadeInSettings,
                onComplete: () =>
                {
                    loginUI.gameObject.SetActive(true);
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: loginUICanvasGroup,
                        settings: loginUIFadeInSettings,
                        onComplete: () =>
                        {
                            loginUI.Open(
                                onLogin: () =>
                                {
                                    CloseLoginUI(
                                        onComplete: () =>
                                        {
                                            onLogin.Invoke();
                                        });
                                },
                                onRegister: () =>
                                {
                                    CloseLoginUI(
                                        onComplete: () =>
                                        {
                                            Debug.Log("註冊介面尚未製作，返回主介面。");
                                        });
                                },
                                onReturn: () =>
                                {
                                    CloseLoginUI(
                                        onComplete: () =>
                                        {
                                        });
                                });
                        });
                });
        }

        private void CloseLoginUI(Action onComplete)
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: loginUICanvasGroup,
                settings: loginUIFadeOutSettings,
                onComplete: () =>
                {
                    loginUI.gameObject.SetActive(false);
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: maskCanvasGroup,
                        settings: maskFadeOutSettings,
                        onComplete: () =>
                        {
                            maskCanvasGroup.gameObject.SetActive(false);
                            onComplete.Invoke();
                        });
                });
        }
    }
}