using DG.Tweening;
using UnityEngine;

namespace System.Battle.System.Child.Initiative_System.System.Child
{
    internal sealed class ScreenMaskSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private CanvasGroup ScreenMask;
        
        private const float FadeDuration = .5f;
        
        private Tween FadeTween;

        public Tween FadeIn()
        {
            FadeTween?.Kill();

            FadeTween = ScreenMask
                .DOFade(1, FadeDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => ScreenMask.alpha = 1)
                .OnKill(() => FadeTween = null);
            
            return FadeTween;
        }

        public Tween FadeOut()
        {
            FadeTween?.Kill();
            
            FadeTween = ScreenMask
                .DOFade(0, FadeDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => ScreenMask.alpha = 0)
                .OnKill(() => FadeTween = null);
            
            return FadeTween;
        }
    }
}