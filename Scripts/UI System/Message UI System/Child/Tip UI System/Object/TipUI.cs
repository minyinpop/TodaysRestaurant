using System;
using Common.Object;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Tip_UI_System.Object
{
    public sealed class TipUI : MonoBehaviour
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
            if (onConfirm == null)
            {
                Debug.Log($"{nameof(TipUI)} > {nameof(ShowMessage)} > {nameof(onConfirm)} callback cannot be null.");
                return;
            }

            content.GetValues(out var message, out var confirmButtonTitle , out _, out _);
            messageTMP.SetText(message);
            confirmButton.SetTitle(confirmButtonTitle);

            confirmButton.OnClicked += onConfirm;
            _confirmButtonCleanupAction = () =>
            {
                confirmButton.OnClicked -= onConfirm;
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