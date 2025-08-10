using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BATTLE.SYSTEM.SCREEN_MASK
{
    internal class ScreenMaskSystem : MonoBehaviour
    {
        [field: SerializeField] private Color DefaultColor;
        [field: SerializeField] private GameObject ScreenMask;
        [field: SerializeField] private Image ScreenMaskImage;
        [field: SerializeField] private CanvasGroup ScreenMaskCanvasGroup;

        private Tween FadeTween;

        public void Show(System.Action onComplete = null)
        {
            KillTween();

            FadeTween = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    ScreenMask.SetActive(true);
                })
                .Append(ScreenMaskCanvasGroup
                    .DOFade(1, 1)
                    .SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        public void Hide(System.Action onComplete = null)
        {
            KillTween();
            
            FadeTween = DOTween.Sequence()
                .Append(ScreenMaskCanvasGroup
                    .DOFade(0, 1)
                    .SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    ScreenMask.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        private void KillTween()
        {
            FadeTween?.Kill();
            FadeTween = null;
        }
    }
}