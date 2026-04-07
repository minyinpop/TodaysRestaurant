using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Value;
using Restaurant_System.Object.Cookware.System.State_Machine.State;
using UI_System.Message_UI_System.Child.Dialogue_Skip_UI_System.Object;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Dialogue_Skip_UI_System.System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class DialogueSkipUISystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Mask")]
        [field: SerializeField] private CanvasGroup mask;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeOutSettings;
        
        [field: Header("UI")]
        [field: SerializeField] private CanvasGroup dialogueSkipUI;
        [field: SerializeField] private DoFade_CanvasGroup dialogueSkipUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup dialogueSkipUIFadeOutSettings;

        private void Awake()
        {
            if (mask is null)
            {
                Debug.Log($"{nameof(DialogueSkipUISystem)} > {nameof(mask)} cannot be null.)");
                return;
            }

            if (dialogueSkipUI is null)
            {
                Debug.Log($"{nameof(DialogueSkipUISystem)} > {nameof(dialogueSkipUI)} cannot be null.)");
            }
        }

        public void ShowUI(PopUpUIContent content, Action onConfirm, Action onCancel = null)
        {
            if (onConfirm is null)
            {
                Debug.Log($"{nameof(DialogueSkipUISystem)} > {nameof(ShowUI)} > {nameof(onConfirm)} callback cannot be null.");
                return;
            }
            
            mask.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: mask,
                settings: maskFadeInSettings,
                onComplete: () =>
                {
                    dialogueSkipUI.GetComponent<DialogueSkipUI>().ShowMessage(content, OnConfirmButtonClicked, OnCancelButtonClicked);
                    dialogueSkipUI.gameObject.SetActive(true);
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: dialogueSkipUI,
                        settings: dialogueSkipUIFadeInSettings,
                        onComplete: () =>
                        {
                            
                        });
                });
            return;
            
            void OnConfirmButtonClicked()
            {
                HideUI(onConfirm.Invoke);
            }
            
            void OnCancelButtonClicked()
            {
                HideUI(() => onCancel?.Invoke());
            }
        }

        private void HideUI(Action onComplete)
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: dialogueSkipUI,
                settings: dialogueSkipUIFadeOutSettings,
                onComplete: () =>
                {
                    dialogueSkipUI.gameObject.SetActive(false);
                    dialogueSkipUI.GetComponent<DialogueSkipUI>().ClearMessage();
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: mask,
                        settings: maskFadeOutSettings,
                        onComplete: () =>
                        {
                            mask.gameObject.SetActive(false);
                            
                            onComplete?.Invoke();
                        });
                });
        }
    }
}