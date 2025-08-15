using DG.Tweening;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM.OBJECT
{
    internal class ScreenMask : MonoBehaviour
    {
        [field: SerializeField] private CanvasGroup CanvasGroup;

        private Tween FadeTween;
        
        private const float FadeDuration = .5f;

        public void Show(System.Action onComplete)
        {
            KillTween();

            FadeTween = ShowScreenMask()
                .OnComplete(() => onComplete());
        }

        public void Hide(System.Action onComplete)
        {
            KillTween();
            
            FadeTween = HideScreenMask()
                .OnComplete(() => onComplete());
        }
        
        #region Tween
            private Tween ShowScreenMask()
            {
                return CanvasGroup
                    .DOFade(1, FadeDuration)
                    .SetEase(Ease.Linear);
            }

            private Tween HideScreenMask()
            {
                return CanvasGroup
                    .DOFade(0, FadeDuration)
                    .SetEase(Ease.Linear);
            }

            private void KillTween()
            {
                FadeTween?.Kill();
                FadeTween = null;
            }
        #endregion
    }
}