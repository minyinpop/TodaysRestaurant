using System.Collections;
using System.Collections.Generic;
using System.Message.Object;
using Data.Animation.DOTween.Basic;
using Data.General;
using DG.Tweening;
using UnityEngine;

namespace System.Message.System.Child
{
    internal sealed class DefeatSystem : MonoBehaviour
    {
        [field: SerializeField] private PopUpUI PopUpUI;

        private IEnumerator ShowCor;

        private readonly Queue<Action> ActiveActions = new();

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }

        public void Show(PopUpUIContent content, Action onConfirm)
        {
            PopUpUI.ShowMask(
                settings: new DoFade_CanvasGroup(
                    endValue: 1,
                    duration: 3,
                    ease: Ease.Linear),
                onComplete: () =>
                {
                    PopUpUI.ShowUI(
                        content: content,
                        settings: new DoFade_CanvasGroup(
                            endValue: 1,
                            duration: 1.5f,
                            ease: Ease.Linear),
                        onComplete: () =>
                        {
                            PopUpUI.SetButtonInteractable(true);
                            PopUpUI.OnClickConfirmButton += onConfirm;
                            ActiveActions.Enqueue(() =>
                            {
                                PopUpUI.SetButtonInteractable(false);
                                PopUpUI.OnClickConfirmButton -= onConfirm;
                            });
                        });
                });
        }
    }
}