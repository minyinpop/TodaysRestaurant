using System;
using DG.Tweening;
using UnityEngine;

namespace Initiative_System.System.Child
{
    internal sealed class ScreenMaskSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private CanvasGroup ScreenMask;
        
        private Tween FadeTween;

        public Tween FadeIn(Action onComplete = null)
        {
            FadeTween?.Kill();

            FadeTween = ScreenMask
                .DOFade(1, .5f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    ScreenMask.alpha = 1;
                    onComplete?.Invoke();
                })
                .OnKill(() => FadeTween = null);
            
            return FadeTween;
        }
    }
}