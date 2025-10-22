using System.Collections;
using System.Message.Object;
using Data.Animation.DOTween.Basic;
using Data.General;
using DG.Tweening;
using UnityEngine;

namespace System.Message.System.Child
{
    internal sealed class TipSystem : MonoBehaviour
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