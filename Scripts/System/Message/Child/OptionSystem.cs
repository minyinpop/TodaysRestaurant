using System.Collections;
using Data.Animation.DOTween.Basic;
using Data.General;
using DG.Tweening;
using General.Object;
using UnityEngine;

namespace System.Message.Child
{
    internal sealed class OptionSystem : MonoBehaviour
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

        public void Show(PopUpUIContent content, Action onClose)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                var close = false;
                PopUpUI.Show(
                    content: new PopUpUIContent(
                        message: string.Empty,
                        confirmButtonTitle: string.Empty,
                        cancelButtonTitle: string.Empty,
                        closeButtonTitle: string.Empty),
                    settings: new DoFade_CanvasGroup(
                        endValue: 1,
                        duration: .2f,
                        ease: Ease.Linear),
                    onComplete: () =>
                    {
                        PopUpUI.OnClickCloseButton += OnCloseButtonClicked;
                        PopUpUI.SetButtonInteractable(true);
                    });
                yield return new WaitUntil(() => close);
                PopUpUI.OnClickCloseButton -= OnCloseButtonClicked;
                PopUpUI.SetButtonInteractable(false);
                PopUpUI.Hide(
                    settings: new DoFade_CanvasGroup(
                        endValue: 0,
                        duration: .2f,
                        ease: Ease.Linear),
                    onComplete: () =>
                    {
                    });
                onClose?.Invoke();
                yield break;
                
                void OnCloseButtonClicked()
                {
                    close = true;
                }
            }
        }
    }
}