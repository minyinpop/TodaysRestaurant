using System;
using Common.Object;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.Object
{
    internal sealed class ItemGetUI : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private TextMeshProUGUI messageTMP;
        [field: SerializeField] private Button confirmButton;

        private Action _onConfirmButtonCleanupAction;

        private void OnDestroy()
        {
            _onConfirmButtonCleanupAction?.Invoke();
        }
        
        public void ShowMessage(PopUpUIContent content, Action onConfirm)
        {
            if (onConfirm == null)
            {
                Debug.Log($"{nameof(ItemGetUI)} > {nameof(ShowMessage)} > {nameof(onConfirm)} callback cannot be null.");
                return;
            }

            content.GetValues(out var message, out var confirmButtonTitle, out _, out _);
            messageTMP.SetText(message);
            confirmButton.SetTitle(confirmButtonTitle);

            confirmButton.OnClick += onConfirm;
            _onConfirmButtonCleanupAction = () =>
            {
                confirmButton.OnClick -= onConfirm;
                _onConfirmButtonCleanupAction = null;
            };
        }

        public void ClearMessage()
        {
            messageTMP.SetText(string.Empty);
            confirmButton.SetTitle(string.Empty);
            
            _onConfirmButtonCleanupAction?.Invoke();
        }
    }
}