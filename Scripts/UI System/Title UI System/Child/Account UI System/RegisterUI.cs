using System;
using System.Collections;
using Common.Button;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace UI_System.Title_UI_System.Child.Account_UI_System
{
    public sealed class RegisterUI : MonoBehaviour
    {
        [field: Header("Input Field")]
        [field: SerializeField] private TMP_InputField mailInputField;
        [field: SerializeField] private TMP_InputField usernameInputField;
        [field: SerializeField] private TMP_InputField passwordInputField;
        
        [field: Header("Error Result Text")]
        [field: SerializeField] private TextMeshProUGUI mailErrorResultText;
        [field: SerializeField] private TextMeshProUGUI usernameErrorResultText;
        [field: SerializeField] private TextMeshProUGUI passwordErrorResultText;
        
        [field: Header("Button")]
        [field: SerializeField] private Button registerButton;
        [field: SerializeField] private Button returnButton;
        
        public event Action OnClickRegisterButtonEvent;
        public event Action OnClickReturnButtonEvent;
        
        private const float _buttonCooldownTime = 3f;
        private IEnumerator _buttonCooldownCoroutine;

        private void Awake()
        {
            #region 輸入框
                if (mailInputField is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(mailInputField)} cannot be null.");
                }
                
                if (usernameInputField is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(usernameInputField)} cannot be null.");
                }
                
                if (passwordInputField is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(passwordInputField)} cannot be null.");
                }
            #endregion

            #region 文字提示
                if (mailErrorResultText is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(mailErrorResultText)} cannot be null.");
                }

                if (usernameErrorResultText is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(usernameErrorResultText)} cannot be null.");
                }

                if (passwordErrorResultText is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(passwordErrorResultText)} cannot be null.");
                }
            #endregion

            #region 按鈕
                if (registerButton is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(registerButton)} cannot be null.");
                }
                
                if (returnButton is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(returnButton)} cannot be null.");
                }
            #endregion
            
            registerButton.OnClick += OnClickRegisterButton;
            returnButton.OnClick += OnClickReturnButton;
        }

        private void OnDisable()
        {
            mailInputField.text = string.Empty;
            usernameInputField.text = string.Empty;
            passwordInputField.text = string.Empty;
            
            mailErrorResultText.text = string.Empty;
            usernameErrorResultText.text = string.Empty;
            passwordErrorResultText.text = string.Empty;
            
            registerButton.SetInteractable(false);
            returnButton.SetInteractable(false);
            
            if (_buttonCooldownCoroutine is not null)
            {
                StopCoroutine(_buttonCooldownCoroutine);
                _buttonCooldownCoroutine = null;
            }
        }
        
        private void OnDestroy()
        {
            registerButton.OnClick -= OnClickRegisterButton;
            returnButton.OnClick -= OnClickReturnButton;
        }

        public void OnOpenEvent()
        {
            registerButton.SetInteractable(true);
            returnButton.SetInteractable(true);
        }

        private void OnClickRegisterButton()
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
                mailErrorResultText.text = string.Empty;
                usernameErrorResultText.text = string.Empty;
                passwordErrorResultText.text = string.Empty;
            #endregion

            var request = new RegisterPlayFabUserRequest
            {
                Email = mailInputField.text,
                Username = usernameInputField.text,
                Password = passwordInputField.text,
                RequireBothUsernameAndEmail = true,
            };
            
            PlayFabClientAPI.RegisterPlayFabUser(
                request: request,
                resultCallback: result =>
                {
                    if (OnClickRegisterButtonEvent is null)
                    {
                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickRegisterButtonEvent)} cannot be null.");
                    }
                    
                    OnClickRegisterButtonEvent.Invoke();
                },
                errorCallback: error =>
                {
                    #region 檢查格式
                        var details = error.ErrorDetails;

                        foreach (var detail in details)
                        {
                            var key = detail.Key;
                            
                            switch (key)
                            {
                                case "Email":
                                {
                                    mailErrorResultText.text = "信箱的格式不正確";
                                    continue;
                                }
                                case "Username":
                                {
                                    usernameErrorResultText.text = "使用者名稱需要在 3 個字元以上";
                                    continue;
                                }
                                case "Password":
                                {
                                    passwordErrorResultText.text = "密碼需要在 6 個字元以上";
                                    continue;
                                }
                            }
                        }
                    #endregion
                    
                    #region 檢查是否已被註冊
                        switch (error.Error)
                        {
                            case PlayFabErrorCode.EmailAddressNotAvailable:
                            {
                                mailErrorResultText.text = "該信箱已被註冊";
                                break;
                            }
                            case PlayFabErrorCode.UsernameNotAvailable:
                            {
                                usernameErrorResultText.text = "該使用者名稱已被註冊";
                                break;
                            }
                        }
                    #endregion
                    
                    #region 開啟所有按鈕的互動
                        StartButtonCooldown();
                    #endregion
                });
        }

        private void OnClickReturnButton()
        {
            if (OnClickReturnButtonEvent is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickReturnButtonEvent)} cannot be null.");
            }
            
            OnClickReturnButtonEvent.Invoke();
        }
        
        private void SetAllButtonInteractable(bool interactable)
        {
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