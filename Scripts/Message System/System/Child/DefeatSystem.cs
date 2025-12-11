using System;
using System.Collections;
using System.Collections.Generic;
using Animation_System.DOTween.Basic;
using Common.Value;
using DG.Tweening;
using Message_System.Object;
using UnityEngine;

namespace Message_System.System.Child
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