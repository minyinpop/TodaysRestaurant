using System;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using Button = Common.Button.Button;

namespace UI_System.Title_UI_System.Child.Account_UI_System
{
    public sealed class LoginUI : MonoBehaviour
    {
        [field: Header("Input Field")]
        [field: SerializeField] private TMP_InputField accountInputField;
        [field: SerializeField] private TMP_InputField passwordInputField;
        
        [field: Header("Button")]
        [field: SerializeField] private Button loginButton;
        [field: SerializeField] private Button registerButton;
        [field: SerializeField] private Button returnButton;

        public event Action OnClickLoginButtonEvent;
        public event Action OnClickRegisterButtonEvent;
        public event Action OnClickReturnButtonEvent;

        private void Awake()
        {
            if (accountInputField is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(accountInputField)} cannot be null.");
            }
            
            if (passwordInputField is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(passwordInputField)} cannot be null.");
            }

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
            
            loginButton.OnClick += OnClickLoginButton;
            registerButton.OnClick += OnClickRegisterButton;
            returnButton.OnClick += OnClickReturnButton;
        }

        private void OnDisable()
        {
            accountInputField.text = string.Empty;
            passwordInputField.text = string.Empty;
            
            loginButton.SetInteractable(false);
            registerButton.SetInteractable(false);
            returnButton.SetInteractable(false);
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
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            {
                throw new InvalidOperationException("請先設定 TitleId。");
            }
            
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
                        
                        OnClickLoginButtonEvent.Invoke();
                    },
                    errorCallback: error =>
                    {
                        Debug.Log($"清求 Mail 登入失敗：{error.ErrorMessage}");
                    });
            }
            else
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
                        
                        OnClickLoginButtonEvent.Invoke();
                    },
                    errorCallback: error =>
                    {
                        Debug.Log($"清求 Username 登入失敗：{error.ErrorMessage}");
                    });
            }
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
    }
}