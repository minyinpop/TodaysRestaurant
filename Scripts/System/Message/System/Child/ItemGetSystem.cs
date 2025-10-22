using System.Collections;
using System.Collections.Generic;
using System.Message.Object;
using Data.Animation.DOTween.Basic;
using Data.General;
using Data.Item.Type.Ingredient;
using DG.Tweening;
using UnityEngine;

namespace System.Message.System.Child
{
    internal sealed class ItemGetSystem : MonoBehaviour
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
                            });
                    });
                yield return new WaitUntil(() => confirm);
                PopUpUI.OnClickConfirmButton -= OnConfirmButtonClicked;
                PopUpUI.SetButtonInteractable(false);
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