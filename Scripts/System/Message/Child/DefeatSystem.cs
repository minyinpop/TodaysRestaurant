using System.Collections;
using Data.Animation.DOTween.Basic;
using Data.General;
using DG.Tweening;
using General.Object;
using UnityEngine;

namespace System.Message.Child
{
    internal sealed class DefeatSystem : MonoBehaviour
    {
        [field: SerializeField] private PopUpUI PopUpUI;

        private IEnumerator ShowCor;

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
                            endValue: 0,
                            duration: 1.5f,
                            ease: Ease.Linear),
                        onComplete: () =>
                        {
                            PopUpUI.SetButtonInteractable(true);
                        });
                });
        }
    }
}