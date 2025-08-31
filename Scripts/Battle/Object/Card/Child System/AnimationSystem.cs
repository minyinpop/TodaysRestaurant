using System;
using Data.DOTween_Value;
using DG.Tweening;
using UnityEngine;

namespace Battle.Object.Card.Child_System
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;

        private Tween MoveTween;

        public void MoveTo(DoAnchorPosValue DoAnchorPosValue, Action OnComplete = null)
        {
            MoveTween?.Kill();
            DoAnchorPosValue.GetValues(out Vector3 EndValue, out float Duration, out bool Snapping, out Ease Ease);
            MoveTween = Rect
                .DOAnchorPos(EndValue, Duration, Snapping)
                .SetEase(Ease)
                .OnComplete(() =>
                {
                    Rect.anchoredPosition = EndValue;
                    OnComplete?.Invoke();
                })
                .OnKill(() => MoveTween = null);
        }
    }
}