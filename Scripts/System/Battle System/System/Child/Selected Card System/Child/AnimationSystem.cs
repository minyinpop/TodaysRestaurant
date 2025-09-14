using DG.Tweening;
using UnityEngine;

namespace System.Battle_System.System.Child.Selected_Card_System.Child
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private CanvasGroup UICanvasGroup;

        private Tween FadeTween;

        public Tween FadeIn()
        {
            FadeTween?.Kill();
            FadeTween = UICanvasGroup
                .DOFade(1, .5f)
                .SetEase(Ease.Linear)
                .OnKill(() => FadeTween = null);
            return FadeTween;
        }

        public Tween FadeOut()
        {
            FadeTween?.Kill();
            FadeTween = UICanvasGroup
                .DOFade(0, .5f)
                .SetEase(Ease.Linear)
                .OnKill(() => FadeTween = null);
            return FadeTween;
        }
    }
}