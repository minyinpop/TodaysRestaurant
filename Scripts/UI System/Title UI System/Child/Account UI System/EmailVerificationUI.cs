using System;
using Common.Button;
using TMPro;
using UnityEngine;

namespace UI_System.Title_UI_System.Child.Account_UI_System
{
    public sealed class EmailVerificationUI : MonoBehaviour
    {
        [field: Header("Input Field")]
        [field: SerializeField] private TMP_InputField codeInputField;
        
        [field: Header("Error Result Text")]
        [field: SerializeField] private TextMeshProUGUI codeErrorResultText;
        
        [field: Header("Button")]
        [field: SerializeField] private Button confirmButton;
        
        public event Action OnClickConfirmButtonEvent;

        private void Awake()
        {
            #region 輸入框
                if (codeInputField is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(codeInputField)} cannot be null.");
                }
            #endregion
            
            #region 文字提示
                if (codeErrorResultText is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(codeErrorResultText)} cannot be null.");
                }
            #endregion

            #region 按鈕
                if (confirmButton is null)
                {
                    throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(confirmButton)} cannot be null.");
                }
            #endregion
            
            confirmButton.OnClick += OnClickConfirmButton;
        }

        private void OnDisable()
        {
            codeInputField.text = string.Empty;
            
            codeErrorResultText.text = string.Empty;
            
            confirmButton.SetInteractable(false);
        }

        private void OnDestroy()
        {
            confirmButton.OnClick -= OnClickConfirmButton;
        }

        public void OnOpenEvent()
        {
            confirmButton.SetInteractable(true);
        }

        private void OnClickConfirmButton()
        {
        }
    }
}