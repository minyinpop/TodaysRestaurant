using System;
using DG.Tweening;
using DoTween_Settings;
using UnityEngine;

namespace Battle_System.Card_System.Animation_System
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;

        private Tween MoveTween;

        public void MoveTo(DoAnchorPosSettings Settings, Action OnComplete = null)
        {
            MoveTween?.Kill();
            
            Settings.GetValues(out Vector3 TargetPosition, out float Duration, out bool Snapping, out Ease Ease);

            MoveTween = Rect
                .DOAnchorPos(TargetPosition, Duration, Snapping)
                .SetEase(Ease)
                .OnComplete(() =>
                {
                    Rect.anchoredPosition = TargetPosition;
                    OnComplete?.Invoke();
                })
                .OnKill(() => MoveTween = null);
        }
    }
}