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

        public void Show()
        {
            KillTween();

            FadeTween = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    ScreenMask.SetActive(true);
                })
                .Append(ScreenMaskCanvasGroup
                    .DOFade(1, 1)
                    .SetEase(Ease.OutQuad));
        }

        public void Show(Color maskColor)
        {
            KillTween();

            FadeTween = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    ScreenMask.SetActive(true);
                    ScreenMaskImage.color = maskColor;
                })
                .Append(ScreenMaskCanvasGroup
                    .DOFade(1, 1)
                    .SetEase(Ease.OutQuad));
        }

        public void Hide()
        {
            KillTween();
            
            FadeTween = DOTween.Sequence()
                .Append(ScreenMaskCanvasGroup
                    .DOFade(0, 1)
                    .SetEase(Ease.OutQuad))
                .AppendCallback(() =>
                {
                    ScreenMask.SetActive(false);
                });
        }

        private void KillTween()
        {
            FadeTween?.Kill();
            FadeTween = null;
        }
    }
}