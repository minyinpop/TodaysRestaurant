using System;
using Common.Value;
using UI_System.Message_UI_System.Child.Switch_UI_System.Object;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Switch_UI_System.System
{
    public sealed class SwitchUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private SwitchUI switchUI;

        private void Awake()
        {
            if (mask == null)
            {
                Debug.Log($"{nameof(SwitchUISystem)} > {nameof(mask)} cannot be null.)");
                return;
            }

            if (switchUI == null)
            {
                Debug.Log($"{nameof(SwitchUISystem)} > {nameof(switchUI)} cannot be null.)");
            }
        }

        public void ShowUI(PopUpUIContent content, Action onConfirm, Action onCancel = null)
        {
            if (onConfirm == null)
            {
                Debug.Log($"{nameof(SwitchUISystem)} > {nameof(ShowUI)} > {nameof(onConfirm)} callback cannot be null.");
                return;
            }
            
            mask.SetActive(true);

            switchUI.ShowMessage(content, OnConfirmButtonClicked, OnCancelButtonClicked);
            switchUI.gameObject.SetActive(true);
            return;
            
            void OnConfirmButtonClicked()
            {
                Cleanup();
                onConfirm.Invoke();
            }
            
            void OnCancelButtonClicked()
            {
                Cleanup();
                onCancel?.Invoke();
            }

            void Cleanup()
            {
                mask.SetActive(false);
                
                switchUI.gameObject.SetActive(false);
                switchUI.ClearMessage();
            }
        }
    }
}