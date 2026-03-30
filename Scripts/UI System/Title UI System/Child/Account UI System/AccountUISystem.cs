using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item.Data;
using UnityEngine;

namespace UI_System.Title_UI_System.Child.Account_UI_System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class AccountUISystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("mask")]
        [field: SerializeField] private CanvasGroup maskCanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeOutSettings;
        
        [field: Header("Login UI")]
        [field: SerializeField] private LoginUI loginUI;
        [field: SerializeField] private CanvasGroup loginUICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup loginUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup loginUIFadeOutSettings;
        
        [field: Header("Register UI")]
        [field: SerializeField] private RegisterUI registerUI;
        [field: SerializeField] private CanvasGroup registerUICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup registerUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup registerUIFadeOutSettings;
        
        [field: Header("Email Verification UI")]
        [field: SerializeField] private EmailVerificationUI mailVerificationUI;
        [field: SerializeField] private CanvasGroup mailVerificationUICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup mailVerificationUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup mailVerificationUIFadeOutSettings;

        public event Action OnLoginSuccess;

        private void Awake()
        {
            #region 檢查必要的條件
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

                if (registerUI is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(registerUI)} cannot be null.");
                }

                if (registerUICanvasGroup is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(registerUICanvasGroup)} cannot be null.");
                }
                
                if (mailVerificationUI is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(mailVerificationUI)} cannot be null.");
                }
                
                if (mailVerificationUICanvasGroup is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(mailVerificationUICanvasGroup)} cannot be null.");
                }
            #endregion
            
            #region 防止小錯誤
                maskCanvasGroup.gameObject.SetActive(false);
                loginUICanvasGroup.gameObject.SetActive(false);
                registerUICanvasGroup.gameObject.SetActive(false);
                mailVerificationUICanvasGroup.gameObject.SetActive(false);
            #endregion
            
            #region 訂閱登入介面的事件
                loginUI.OnClickLoginButtonEvent += OnClickLoginButtonFromLoginUI;
                loginUI.OnClickRegisterButtonEvent += OnClickRegisterButtonFromLoginUI;
                loginUI.OnClickReturnButtonEvent += OnClickReturnButtonFromLoginUI;
            #endregion
            
            #region 訂閱註冊介面的事件
                registerUI.OnClickRegisterButtonEvent += OnClickRegisterButtonFromRegisterUI;
                registerUI.OnClickReturnButtonEvent += OnClickReturnButtonFromRegisterUI;
            #endregion
        }
        
        private void OnDestroy()
        {
            loginUI.OnClickLoginButtonEvent -= OnClickLoginButtonFromLoginUI;
            loginUI.OnClickRegisterButtonEvent -= OnClickRegisterButtonFromLoginUI;
            loginUI.OnClickReturnButtonEvent -= OnClickReturnButtonFromLoginUI;
            
            registerUI.OnClickRegisterButtonEvent -= OnClickRegisterButtonFromRegisterUI;
            registerUI.OnClickReturnButtonEvent -= OnClickReturnButtonFromRegisterUI;
        }

        public void OpenLoginUI()
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
                            loginUI.OnOpenEvent();
                        });
                });
        }

        #region 登入介面
            private void OnClickLoginButtonFromLoginUI()
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

                                #region 登入成功後的事件
                                    ItemDatabase.Initialize();
                                #endregion

                                #region 發送登入成功訊息
                                    if (OnLoginSuccess is null)
                                    {
                                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnLoginSuccess)} cannot be null.");
                                    }

                                    OnLoginSuccess.Invoke();
                                #endregion
                            });
                    });
            }

            private void OnClickRegisterButtonFromLoginUI()
            {
                animation.DoFade_CanvasGroup(
                    canvasGroup: loginUICanvasGroup,
                    settings: loginUIFadeOutSettings,
                    onComplete: () =>
                    {
                        loginUICanvasGroup.gameObject.SetActive(false);
                        registerUI.gameObject.SetActive(true);
                        
                        animation.DoFade_CanvasGroup(
                            canvasGroup: registerUICanvasGroup,
                            settings: registerUIFadeInSettings,
                            onComplete: () =>
                            {
                                registerUI.OnOpenEvent();
                            });
                    });
            }

            private void OnClickReturnButtonFromLoginUI()
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
                            });
                    });
            }
        #endregion

        #region 註冊介面
            private void OnClickRegisterButtonFromRegisterUI()
            {
                animation.DoFade_CanvasGroup(
                    canvasGroup: registerUICanvasGroup,
                    settings: registerUIFadeOutSettings,
                    onComplete: () =>
                    {
                        registerUI.gameObject.SetActive(false);
                        loginUI.gameObject.SetActive(true);
                        
                        animation.DoFade_CanvasGroup(
                            canvasGroup: loginUICanvasGroup,
                            settings: loginUIFadeInSettings,
                            onComplete: () =>
                            {
                                loginUI.OnOpenEvent();
                            });
                    });
            }
            
            private void OnClickReturnButtonFromRegisterUI()
            {
                animation.DoFade_CanvasGroup(
                    canvasGroup: registerUICanvasGroup,
                    settings: registerUIFadeOutSettings,
                    onComplete: () =>
                    {
                        registerUI.gameObject.SetActive(false);
                        loginUI.gameObject.SetActive(true);
                        
                        animation.DoFade_CanvasGroup(
                            canvasGroup: loginUICanvasGroup,
                            settings: loginUIFadeInSettings,
                            onComplete: () =>
                            {
                                loginUI.OnOpenEvent();
                            });
                    });
            }
        #endregion
        
        #region 信箱認證介面
        #endregion
    }
}