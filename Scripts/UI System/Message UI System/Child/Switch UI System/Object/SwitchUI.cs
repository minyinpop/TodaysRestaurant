using System;
using Common.Button;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Switch_UI_System.Object
{
    public sealed class SwitchUI : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private TextMeshProUGUI messageTMP;
        [field: SerializeField] private Button confirmButton;
        [field: SerializeField] private Button cancelButton;

        private Action _confirmButtonCleanupAction;
        private Action _cancelButtonCleanupAction;

        private void OnDestroy()
        {
            _confirmButtonCleanupAction?.Invoke();
            _cancelButtonCleanupAction?.Invoke();
        }

        public void ShowMessage(PopUpUIContent content, Action onConfirm, Action onCancel)
        {
            if (onConfirm == null)
            {
                Debug.Log($"{nameof(SwitchUI)} > {nameof(ShowMessage)} > {nameof(onConfirm)} callback cannot be null.");
                return;
            }

            if (onCancel == null)
            {
                Debug.Log($"{nameof(SwitchUI)} > {nameof(ShowMessage)} > {nameof(onCancel)} callback cannot be null.");
                return;
            }

            content.GetValues(out var message, out var confirmButtonTitle , out var cancelButtonTitle, out _);
            messageTMP.SetText(message);
            confirmButton.SetTitle(confirmButtonTitle);
            cancelButton.SetTitle(cancelButtonTitle);

            confirmButton.OnClick += onConfirm;
            _confirmButtonCleanupAction = () =>
            {
                confirmButton.OnClick -= onConfirm;
                _confirmButtonCleanupAction = null;
            };
            
            cancelButton.OnClick += onCancel;
            _cancelButtonCleanupAction = () =>
            {
                cancelButton.OnClick -= onCancel;
                _cancelButtonCleanupAction = null;
            };
        }

        public void ClearMessage()
        {
            messageTMP.SetText(string.Empty);
            confirmButton.SetTitle(string.Empty);
            cancelButton.SetTitle(string.Empty);
            
            _confirmButtonCleanupAction?.Invoke();
            _cancelButtonCleanupAction?.Invoke();
        }
    }
}