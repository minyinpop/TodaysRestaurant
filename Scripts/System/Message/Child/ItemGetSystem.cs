using System.Collections;
using System.Collections.Generic;
using Data.Animation.DOTween.Basic;
using Data.General;
using Data.Item.Base;
using DG.Tweening;
using General.Object;
using UnityEngine;

namespace System.Message.Child
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

        public void Show(PopUpUIContent content, List<ItemSO> items, Action onConfirm)
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
                                PopUpUI.OnConfirm += OnConfirmButtonClicked;
                                PopUpUI.SetButtonInteractable(true);
                            });
                    });
                yield return new WaitUntil(() => confirm);
                PopUpUI.OnConfirm -= OnConfirmButtonClicked;
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