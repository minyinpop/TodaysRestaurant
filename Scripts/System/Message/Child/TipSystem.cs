using System.Collections;
using System.General.DOTween;
using Data.Animation.DOTween.Basic;
using DG.Tweening;
using General.Object;
using TMPro;
using UnityEngine;

namespace System.Message.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class TipSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("UI")]
        [field: SerializeField] private GameObject TipUI;
        [field: SerializeField] private CanvasGroup TipUICanvasGroup;
        
        [field: Header("Message")]
        [field: SerializeField] private TextMeshProUGUI MessageTMP;
        
        [field: Header("Button")]
        [field: SerializeField] private Button ConfirmButton;

        private IEnumerator ShowCor;
        
        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor);
                ShowCor = null;
            }
        }

        public void Show(string message, Action onConfirm)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                var complete = false;
                TipUI.SetActive(true);
                MessageTMP.text = message;
                DoAnimation.DoFade_CanvasGroup(TipUICanvasGroup, new DoFade_CanvasGroup(1, .15f, Ease.Linear),
                    onComplete: () =>
                    {
                        ConfirmButton.OnClick += OnConfirmButtonClicked;
                        ConfirmButton.SetInteractable(true);
                    });
                yield return new WaitUntil(() => complete);
                ConfirmButton.SetInteractable(false);
                ConfirmButton.OnClick -= OnConfirmButtonClicked;
                DoAnimation.DoFade_CanvasGroup(TipUICanvasGroup, new DoFade_CanvasGroup(0, .15f, Ease.Linear),
                    onComplete: () =>
                    {
                        MessageTMP.text = "";
                        TipUI.SetActive(false);
                        onConfirm?.Invoke();
                    });
                yield break;
                
                void OnConfirmButtonClicked()
                {
                    complete = true;
                }
            }
        }
    }
}