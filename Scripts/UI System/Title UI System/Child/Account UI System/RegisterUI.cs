using System;
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
        
        [field: Header("Button")]
        [field: SerializeField] private Button registerButton;
        [field: SerializeField] private Button returnButton;
        
        public event Action OnClickReturnButtonEvent;

        private void Awake()
        {
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
            
            if (registerButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(registerButton)} cannot be null.");
            }
            
            if (returnButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(returnButton)} cannot be null.");
            }
            
            registerButton.OnClick += OnClickRegisterButton;
            returnButton.OnClick += OnClickReturnButton;
        }

        private void OnDisable()
        {
            mailInputField.text = string.Empty;
            usernameInputField.text = string.Empty;
            passwordInputField.text = string.Empty;
            
            registerButton.SetInteractable(false);
            returnButton.SetInteractable(false);
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
        
        void OnClickRegisterButton()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            {
                throw new InvalidOperationException("請先設定 TitleId。");
            }

            var request = new RegisterPlayFabUserRequest
            {
                Email = mailInputField.text,
                Username = usernameInputField.text,
                Password = passwordInputField.text,
                RequireBothUsernameAndEmail = true
            };
            
            PlayFabClientAPI.RegisterPlayFabUser(
                request: request,
                resultCallback: result =>
                {
                    if (OnClickReturnButtonEvent is null)
                    {
                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickReturnButtonEvent)} cannot be null.");
                    }
            
                    OnClickReturnButtonEvent.Invoke();
                },
                errorCallback: error =>
                {
                    Debug.Log(error.ErrorMessage);
                });
        }
        
        void OnClickReturnButton()
        {
            if (OnClickReturnButtonEvent is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickReturnButtonEvent)} cannot be null.");
            }
            
            OnClickReturnButtonEvent.Invoke();
        }
    }
}