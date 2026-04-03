using System;
using System.Collections;
using Input_System;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = Common.Button.Button;

namespace UI_System.Title_UI_System.Child.Account_UI_System
{
    public sealed class LoginUI : MonoBehaviour
    {
        [field: Header("Input Field")]
        [field: SerializeField] private TMP_InputField accountInputField;
        [field: SerializeField] private TMP_InputField passwordInputField;
        
        [field: Header("Error Result Text")]
        [field: SerializeField] private TextMeshProUGUI loginErrorResultText;
        
        [field: Header("Button")]
        [field: SerializeField] private Button loginButton;
        [field: SerializeField] private Button registerButton;
        [field: SerializeField] private Button returnButton;

        public event Action<bool> OnClickLoginButtonEvent;
        public event Action OnClickRegisterButtonEvent;
        public event Action OnClickReturnButtonEvent;

        private const float _buttonCooldownTime = 3f;
        private IEnumerator _buttonCooldownCoroutine;

        private void Awake()
        {
            #region 輸入框
                if (accountInputField is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(accountInputField)} cannot be null.");
                }
                
                if (passwordInputField is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(passwordInputField)} cannot be null.");
                }
            #endregion
            
            #region 文字提示
                if (loginErrorResultText is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(loginErrorResultText)} cannot be null.");           
                }
            #endregion

            #region 按鈕
                if (loginButton is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(loginButton)} cannot be null.");
                }
                
                if (registerButton is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(registerButton)} cannot be null.");
                }
                
                if (returnButton is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(returnButton)} cannot be null.");
                }
            #endregion
            
            loginButton.OnClick += OnClickLoginButton;
            registerButton.OnClick += OnClickRegisterButton;
            returnButton.OnClick += OnClickReturnButton;
        }

        private void OnEnable()
        {
            accountInputField.Select();
            
            InputSystem.OnPerformedTab += OnClickTabButton;
        }

        private void OnDisable()
        {
            accountInputField.text = string.Empty;
            passwordInputField.text = string.Empty;
            
            loginErrorResultText.text = string.Empty;
            
            loginButton.SetInteractable(false);
            registerButton.SetInteractable(false);
            returnButton.SetInteractable(false);
            
            InputSystem.OnPerformedTab -= OnClickTabButton;

            if (_buttonCooldownCoroutine is not null)
            {
                StopCoroutine(_buttonCooldownCoroutine);
                _buttonCooldownCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            loginButton.OnClick -= OnClickLoginButton;
            registerButton.OnClick -= OnClickRegisterButton;
            returnButton.OnClick -= OnClickReturnButton;
        }

        public void OnOpenEvent()
        {
            loginButton.SetInteractable(true);
            registerButton.SetInteractable(true);
            returnButton.SetInteractable(true);
        }

        private void OnClickLoginButton()
        {
            #region 條件檢查
                if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
                {
                    throw new InvalidOperationException("請先設定 TitleId。");
                }
            #endregion
            
            #region 暫時關閉所有的按鈕互動
                SetAllButtonInteractable(false);
            #endregion
            
            #region 清除提示訊息
                loginErrorResultText.text = string.Empty;
            #endregion
            
            #region 使用電子信箱登入
                if (accountInputField.text.Contains("@"))
                {
                    var request = new LoginWithEmailAddressRequest
                    {
                        Email = accountInputField.text,
                        Password = passwordInputField.text
                    };
                    
                    PlayFabClientAPI.LoginWithEmailAddress(
                        request: request,
                        resultCallback: result =>
                        {
                            if (OnClickLoginButtonEvent is null)
                            {
                                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickLoginButtonEvent)} cannot be null.");
                            }
                            
                            OnClickLoginButtonEvent.Invoke(result.NewlyCreated);
                        },
                        errorCallback: error =>
                        {
                            #region 顯示登入錯誤訊息
                                switch (error.Error)
                                {
                                    case PlayFabErrorCode.InvalidParams:
                                    {
                                        loginErrorResultText.text = "電子信箱或密碼不得為空";
                                        break;
                                    }
                                    case PlayFabErrorCode.InvalidEmailAddress:
                                    {
                                        loginErrorResultText.text = "電子信箱的格式錯誤";
                                        break;
                                    }
                                    case PlayFabErrorCode.AccountNotFound:
                                    {
                                        loginErrorResultText.text = "此帳號不存在";
                                        break;
                                    }
                                    case PlayFabErrorCode.InvalidEmailOrPassword:
                                    {
                                        loginErrorResultText.text = "電子信箱或密碼錯誤";
                                        break;
                                    }
                                    default:
                                    {
                                        Debug.Log("錯誤訊息：");
                                        Debug.Log(error.Error);
                                        break;
                                    }
                                }
                            #endregion
                            
                            #region 開啟所有按鈕的互動
                                StartButtonCooldown();
                            #endregion
                        });
                    return;
                }
            #endregion
            
            #region 使用帳號登入
                if (!accountInputField.text.Contains("@"))
                {
                    var request = new LoginWithPlayFabRequest
                    {
                        Username = accountInputField.text,
                        Password = passwordInputField.text
                    };
                    
                    PlayFabClientAPI.LoginWithPlayFab(
                        request: request,
                        resultCallback: result =>
                        {
                            if (OnClickLoginButtonEvent is null)
                            {
                                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickLoginButtonEvent)} cannot be null.");
                            }
                            
                            OnClickLoginButtonEvent.Invoke(result.NewlyCreated);
                        },
                        errorCallback: error =>
                        {
                            #region 顯示登入錯誤訊息
                                switch (error.Error)
                                {
                                    case PlayFabErrorCode.InvalidParams:
                                    {
                                        loginErrorResultText.text = "帳號或密碼不得為空";
                                        break;
                                    }
                                    case PlayFabErrorCode.AccountNotFound:
                                    {
                                        loginErrorResultText.text = "此帳號不存在";
                                        break;
                                    }
                                    case PlayFabErrorCode.InvalidUsernameOrPassword:
                                    {
                                        loginErrorResultText.text = "帳號或密碼錯誤";
                                        break;
                                    }
                                    case PlayFabErrorCode.APIClientRequestRateLimitExceeded:
                                    {
                                        loginErrorResultText.text = "錯誤太多次 請稍後再嘗試";
                                        break;
                                    }
                                    default:
                                    {
                                        Debug.Log("錯誤訊息：");
                                        Debug.Log(error.Error);
                                        break;
                                    }
                                }
                            #endregion
                            
                            #region 開啟所有按鈕的互動
                                StartButtonCooldown();
                            #endregion
                        });
                }
            #endregion
        }
        
        private void OnClickRegisterButton()
        {
            if (OnClickRegisterButtonEvent is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickRegisterButtonEvent)} cannot be null.");
            }

            OnClickRegisterButtonEvent.Invoke();
        }
        
        private void OnClickReturnButton()
        {
            if (OnClickReturnButtonEvent is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickReturnButtonEvent)} cannot be null.");
            }
            
            OnClickReturnButtonEvent.Invoke();
        }

        private void OnClickTabButton()
        {
            var currentSelectable = EventSystem.current?.currentSelectedGameObject?.GetComponent<Selectable>();
            var nextSelectable = currentSelectable?.FindSelectableOnDown();
                nextSelectable?.Select();
        }

        private void SetAllButtonInteractable(bool interactable)
        {
            loginButton.SetInteractable(interactable);
            registerButton.SetInteractable(interactable);
            returnButton.SetInteractable(interactable);
        }

        private void StartButtonCooldown()
        {
            _buttonCooldownCoroutine = ButtonCooldown();
            StartCoroutine(_buttonCooldownCoroutine);
            return;

            IEnumerator ButtonCooldown()
            {
                yield return new WaitForSeconds(_buttonCooldownTime);
                SetAllButtonInteractable(true);
            }
        }
    }
}