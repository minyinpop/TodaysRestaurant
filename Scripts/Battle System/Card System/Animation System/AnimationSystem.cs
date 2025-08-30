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
        
        [field: Header("Card")]
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;

        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;

        #region Basic Animation
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

            public void RotateTo(DoRotateSettings Settings, Action OnComplete = null)
            {
                RotateTween?.Kill();
                
                Settings.GetValues(out Vector3 TargetValue, out float Duration, out RotateMode RotateMode, out Ease Ease);
                
                RotateTween = Rect
                    .DORotate(TargetValue, Duration, RotateMode)
                    .SetEase(Ease)
                    .OnComplete(() =>
                    {
                        Rect.eulerAngles = TargetValue;
                        OnComplete?.Invoke();
                    })
                    .OnKill(() => RotateTween = null);
            }

            public void ScaleTo(DoScaleSettings Settings, Action OnComplete = null)
            {
                ScaleTween?.Kill();
                
                Settings.GetValues(out Vector3 TargetValue, out float Duration, out Ease Ease);
                
                ScaleTween = Rect
                    .DOScale(TargetValue, Duration)
                    .SetEase(Ease)
                    .OnComplete(() =>
                    {
                        Rect.localScale = TargetValue;
                        OnComplete?.Invoke();
                    })
                    .OnKill(() => ScaleTween = null);
            }
        #endregion
    }
}