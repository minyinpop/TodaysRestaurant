using System;
using Data;
using Data.DOTween_Values;
using DG.Tweening;
using UnityEngine;

namespace Battle_System.Object.Card.System
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Object")]
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;

        private Tween MainSequence;
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        #region Basic Animation
            public void MoveTo(AnchorPosValue value, Action OnComplete = null)
            {
                MoveTween?.Kill();
                value.GetValues(out Vector3 EndValue, out float Duration, out bool Snapping, out Ease Ease);

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

            private void RotateTo()
            {
            }

            private void ScaleTo()
            {
            }
        #endregion
    }
}