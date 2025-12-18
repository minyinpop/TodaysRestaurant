using System;
using System.Collections;
using System.Collections.Generic;
using Animation_System.DOTween.Basic;
using Common.Value;
using DG.Tweening;
using Item.Ingredient;
using Message_System.Object;
using UnityEngine;

namespace Message_System.System.Child
{
    internal sealed class ItemGetSystem : MonoBehaviour
    {
        [field: SerializeField] private PopUpUI PopUpUI;

        private IEnumerator ShowCor;

        private readonly Queue<Action> ActiveActions = new();
        
        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
            if (ShowCor is not null) { StopCoroutine(ShowCor); ShowCor = null; }
        }

        public void Show(PopUpUIContent content, List<IngredientSO> items, Action onConfirm)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                var confirm = false;
                PopUpUI.Show(content, new DoFade_CanvasGroup(1, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        PopUpUI.ShowItem(items,
                            onComplete: () =>
                            {
                                PopUpUI.OnClickConfirmButton += OnConfirmButtonClicked;
                                PopUpUI.SetButtonInteractable(true);
                                ActiveActions.Enqueue(() =>
                                {
                                    PopUpUI.OnClickCancelButton -= OnConfirmButtonClicked;
                                    PopUpUI.SetButtonInteractable(false);
                                });
                            });
                    });
                yield return new WaitUntil(() => confirm);
                PopUpUI.Hide(
                    settings: new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        onConfirm?.Invoke();
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