using System;
using System.Collections;
using Animation_System.DOTween.Basic;
using Common.Value;
using DG.Tweening;
using Message_System.Object;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Message_UI_System
{
    internal sealed class SwitchUISystem : MonoBehaviour, IUISystem
    {
        [field: SerializeField] private PopUpUI PopUpUI;

        private IEnumerator ShowCor;

        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor);
                ShowCor = null;
            }
        }

        public void Show(PopUpUIContent content, Action onShow, Action onConfirm, Action onCancel, Action onClose)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                var complete = false;
                var confirm = false;
                var cancel = false;
                onShow?.Invoke();
                PopUpUI.Show(content, new DoFade_CanvasGroup(1, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        PopUpUI.OnClickConfirmButton += OnConfirmButtonClicked;
                        PopUpUI.OnClickCancelButton += OnCancelButtonClicked;
                        PopUpUI.SetButtonInteractable(true);
                        complete = true;
                    });
                yield return new WaitUntil(() => complete && (confirm || cancel));
                PopUpUI.OnClickConfirmButton -= OnConfirmButtonClicked;
                PopUpUI.OnClickCancelButton -= OnCancelButtonClicked;
                PopUpUI.SetButtonInteractable(false);
                if (confirm)
                    onConfirm?.Invoke();
                else if (cancel)
                    onCancel?.Invoke();
                PopUpUI.Hide(
                    settings: new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        onClose?.Invoke();
                    });
                yield break;
                
                void OnConfirmButtonClicked()
                {
                    confirm = true;
                }
                
                void OnCancelButtonClicked()
                {
                    cancel = true;
                }
            }
        }
    }
}