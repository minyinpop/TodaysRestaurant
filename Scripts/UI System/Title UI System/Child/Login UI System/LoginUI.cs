using System;
using TMPro;
using UnityEngine;
using Button = Common.Button.Button;

namespace UI_System.Title_UI_System.Child.Login_UI_System
{
    public sealed class LoginUI : MonoBehaviour
    {
        [field: Header("Input Field")]
        [field: SerializeField] private TMP_InputField usernameInputField;
        [field: SerializeField] private TMP_InputField passwordInputField;
        
        [field: Header("Button")]
        [field: SerializeField] private Button loginButton;
        [field: SerializeField] private Button registerButton;
        [field: SerializeField] private Button returnButton;
        
        private Action _buttonCleanupAction;

        private void Awake()
        {
            if (usernameInputField is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(usernameInputField)} cannot be null.");
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
        }

        private void OnDestroy()
        {
            if (_buttonCleanupAction is not null)
            {
                _buttonCleanupAction.Invoke();
            }
        }

        public void Open(Action onLogin, Action onRegister, Action onReturn)
        {
            if (_buttonCleanupAction is null)
            {
                loginButton.OnClick += OnClickLoginButton;
                registerButton.OnClick += OnClickRegisterButton;
                returnButton.OnClick += OnClickReturnButton;

                _buttonCleanupAction = () =>
                {
                    loginButton.SetInteractable(false);
                    registerButton.SetInteractable(false);
                    returnButton.SetInteractable(false);
                    
                    loginButton.OnClick -= OnClickLoginButton;
                    registerButton.OnClick -= OnClickRegisterButton;
                    returnButton.OnClick -= OnClickReturnButton;
                    
                    _buttonCleanupAction = null;
                };
            }
            
            loginButton.SetInteractable(true);
            registerButton.SetInteractable(true);
            returnButton.SetInteractable(true);
            return;

            void OnClickLoginButton()
            {
                _buttonCleanupAction.Invoke();
                onLogin.Invoke();
            }
            
            void OnClickRegisterButton()
            {
                _buttonCleanupAction.Invoke();
                onRegister.Invoke();
            }
            
            void OnClickReturnButton()
            {
                _buttonCleanupAction.Invoke();
                onReturn.Invoke();
            }
        }
    }
}