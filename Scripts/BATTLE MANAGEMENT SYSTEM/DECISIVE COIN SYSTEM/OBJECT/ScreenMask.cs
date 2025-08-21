using DG.Tweening;
using UnityEngine;

namespace BATTLE_MANAGEMENT_SYSTEM.DECISIVE_COIN_SYSTEM.OBJECT
{
    internal class ScreenMask : MonoBehaviour
    {
        [field: SerializeField] private CanvasGroup CanvasGroup;

        private Tween FadeTween;
        
        private const float FadeDuration = .5f;

        public Tween Show()
        {
            KillTween();
            FadeTween = ShowScreenMask();
            return FadeTween;
        }

        public Tween Hide()
        {
            KillTween();
            FadeTween = HideScreenMask();
            return FadeTween;
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