using System;
using Common.Object;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System.System.Child.Tip_UI_System
{
    public sealed class PopUpUI_Tip : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private TextMeshProUGUI messageTMP;
        [field: SerializeField] private Button confirmButton;

        private Action _confirmButtonCleanupAction;

        private void OnDestroy()
        {
            _confirmButtonCleanupAction?.Invoke();
        }

        public void Initialize(PopUpUIContent content, Action onConfirm)
        {
            if (onConfirm == null)
            {
                Debug.LogError("PopUpUI_Tip > Initialize > onConfirm callback cannot be null.");
                return;
            }

            content.GetValues(out var message, out var confirmButtonTitle , out _, out _);
            messageTMP.SetText(message);
            confirmButton.SetTitle(confirmButtonTitle);

            confirmButton.OnClicked += onConfirm;
            _confirmButtonCleanupAction = () => confirmButton.OnClicked -= onConfirm;
        }
    }
}