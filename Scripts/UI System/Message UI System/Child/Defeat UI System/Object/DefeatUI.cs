using System;
using Common.Button;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Defeat_UI_System.Object
{
    public sealed class DefeatUI : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private TextMeshProUGUI messageTMP;
        [field: SerializeField] private Button confirmButton;

        private Action _confirmButtonCleanupAction;

        private void OnDestroy()
        {
            _confirmButtonCleanupAction?.Invoke();
        }

        public void ShowMessage(PopUpUIContent content, Action onConfirm)
        {
            content.GetValues(
                message: out var message,
                confirmButtonTitle: out var confirmButtonTitle,
                cancelButtonTitle: out _,
                closeButtonTitle: out _);
            messageTMP.SetText(message);
            confirmButton.SetTitle(confirmButtonTitle);

            confirmButton.OnClick += onConfirm;
            _confirmButtonCleanupAction = () =>
            {
                confirmButton.OnClick -= onConfirm;
                _confirmButtonCleanupAction = null;
            };
        }

        public void ClearMessage()
        {
            messageTMP.SetText(string.Empty);
            confirmButton.SetTitle(string.Empty);
            
            _confirmButtonCleanupAction?.Invoke();
        }
    }
}