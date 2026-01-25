using System;
using Common.Object;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System.System.Child.Switch_UI_System
{
    public sealed class PopUpUI_Switch : MonoBehaviour
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

        public void Initialize(PopUpUIContent content, Action onConfirm, Action onCancel)
        {
            if (onConfirm == null)
            {
                Debug.LogError("PopUpUI_Tip > Initialize > onConfirm callback cannot be null.");
                return;
            }

            if (onCancel == null)
            {
                Debug.LogError("PopUpUI_Tip > Initialize > onCancel callback cannot be null.");
                return;
            }

            content.GetValues(out var message, out var confirmButtonTitle , out var cancelButtonTitle, out _);
            messageTMP.SetText(message);
            confirmButton.SetTitle(confirmButtonTitle);
            cancelButton.SetTitle(cancelButtonTitle);

            confirmButton.OnClicked += onConfirm;
            _confirmButtonCleanupAction = () => confirmButton.OnClicked -= onConfirm;
            
            cancelButton.OnClicked += onCancel;
            _cancelButtonCleanupAction = () => cancelButton.OnClicked -= onCancel;
        }
    }
}