using System;
using System.Collections;
using Animation_System.DOTween.Basic;
using Common.Value;
using DG.Tweening;
using Message_System.Object;
using UnityEngine;

namespace UI_System.System.Child.Message_UI_System
{
    internal sealed class TipUISystem : MonoBehaviour
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

        public void Show(PopUpUIContent content, Action onConfirm)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                var confirm = false;
                PopUpUI.Show(
                    content: content,
                    settings: new DoFade_CanvasGroup(
                        endValue: 1,
                        duration: .2f,
                        ease: Ease.Linear),
                    onComplete: () =>
                    {
                        PopUpUI.OnClickConfirmButton += OnConfirmButtonClicked;
                        PopUpUI.SetButtonInteractable(true);
                    });
                yield return new WaitUntil(() => confirm);
                PopUpUI.OnClickConfirmButton -= OnConfirmButtonClicked;
                PopUpUI.SetButtonInteractable(false);
                PopUpUI.Hide(
                    settings: new DoFade_CanvasGroup(
                        endValue: 0,
                        duration: .2f,
                        ease: Ease.Linear),
                    onComplete: () =>
                    {
                        onConfirm?.Invoke();
                        ShowCor = null;
                    });
                yield break;

                void OnConfirmButtonClicked()
                {
                    confirm = true;
                }
            }
        }
    }
}