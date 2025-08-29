using System;
using Animation_Settings;
using DG.Tweening;
using UnityEngine;

namespace Battle.Card.Animation
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;

        private Tween MoveTween;
        
        public void MoveTo(DOAnchorPosSettings Settings, Action OnComplete = null)
        {
            MoveTween?.Kill();
            
            Settings.GetValues(out Vector3 TargetPos, out float Duration, out bool Snapping);
            
            MoveTween = Rect
                .DOAnchorPos(TargetPos, Duration, Snapping)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => Rect.anchoredPosition = TargetPos)
                .OnKill(() => OnComplete?.Invoke());
        }
    }
}