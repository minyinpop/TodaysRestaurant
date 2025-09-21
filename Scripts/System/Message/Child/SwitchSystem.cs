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
    internal sealed class SwitchSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("UI")]
        [field: SerializeField] private GameObject SwitchUI;
        [field: SerializeField] private CanvasGroup SwitchUICanvasGroup;
        
        [field: Header("Message")]
        [field: SerializeField] private TextMeshProUGUI MessageTMP;
        
        [field: Header("Button")]
        [field: SerializeField] private Button ConfirmButton;
        [field: SerializeField] private Button CancelButton;

        private IEnumerator ShowCor;
        
        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor);
                ShowCor = null;
            }
        }

        public void Show(string message, Action onShow, Action onConfirm, Action onCancel, Action onClose)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;
            
            IEnumerator ShowCoroutine()
            {
                var complete = false;
                var isConfirm = false;
                onShow?.Invoke();
                SwitchUI.SetActive(true);
                MessageTMP.text = message;
                DoAnimation.DoFade_CanvasGroup(SwitchUICanvasGroup, new DoFade_CanvasGroup(1, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        ConfirmButton.OnClick += OnConfirmButtonClick;
                        CancelButton.OnClick += OnCancelButtonClick;
                        ConfirmButton.SetInteractable(true);
                        CancelButton.SetInteractable(true);
                    });
                yield return new WaitUntil(() => complete);
                ConfirmButton.SetInteractable(false);
                CancelButton.SetInteractable(false);
                DoAnimation.DoFade_CanvasGroup(SwitchUICanvasGroup, new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        ConfirmButton.OnClick -= OnConfirmButtonClick;
                        CancelButton.OnClick -= OnCancelButtonClick;
                        if (isConfirm)
                            onConfirm?.Invoke();
                        else
                            onCancel?.Invoke();
                        MessageTMP.text = "";
                        SwitchUI.SetActive(false);
                        onClose?.Invoke();
                    });
                yield break;
                
                void OnConfirmButtonClick()
                {
                    complete = true;
                    isConfirm = true;
                }
                
                void OnCancelButtonClick()
                {
                    complete = true;
                    isConfirm = false;
                }
            }
        }
    }
}