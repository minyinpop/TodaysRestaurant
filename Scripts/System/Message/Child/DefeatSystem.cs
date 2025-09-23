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
                settings: new DoFade_CanvasGroup(1, 3, Ease.Linear),
                onComplete: () =>
                {
                    PopUpUI.ShowUI(
                        content: content,
                        settings: new DoFade_CanvasGroup(1, .2f, Ease.Linear),
                        onComplete: () =>
                        {
                            PopUpUI.SetButtonInteractable(true);
                        });
                });
        }
    }
}