using DG.Tweening;
using UnityEngine;

namespace BATTLE.SCREEN_MASK_SYSTEM
{
    internal class ScreenMaskSystem : MonoBehaviour
    {
        [field: SerializeField] private GameObject ScreenMask;
        [field: SerializeField] private CanvasGroup ScreenMaskCanvasGroup;

        private Tween FadeTween;

        private const float FadeTweenDuration = 2f;

        public void Show(System.Action onComplete = null)
        {
            KillTween();
            FadeTween = ScreenMaskCanvasGroup
                .DOFade(1, FadeTweenDuration)
                .SetEase(Ease.Linear)
                .OnStart(() =>
                {
                    ScreenMask.SetActive(true);
                })
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        public void Hide(System.Action onComplete = null)
        {
            KillTween();
            FadeTween = ScreenMaskCanvasGroup
                .DOFade(0, FadeTweenDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    ScreenMask.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        private void KillTween()
        {
            if (FadeTween is null) return;
            FadeTween.Kill();
            FadeTween = null;
        }
    }
}