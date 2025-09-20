using Data.Animation.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Battle.System.Child.Initiative_System.Object.Toss_Result_Text
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        private Tween ScaleTween;

        public Tween ScaleTo(DoScale settings)
        {
            ScaleTween?.Kill();
            
            settings.GetValues(out var endValue, out var duration, out var ease);
            
            ScaleTween = Rect
                .DOScale(endValue, duration)
                .SetEase(ease)
                .OnComplete(() => Rect.localScale = endValue)
                .OnKill(() => ScaleTween = null);
            
            return ScaleTween;
        }
    }
}