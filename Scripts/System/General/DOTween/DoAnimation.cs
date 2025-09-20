using Data.Animation.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.General.DOTween
{
    internal sealed class DoAnimation : MonoBehaviour
    {
        #region Move
            private Tween MoveTween;

            public void DoAnchorPos(RectTransform rect, DoAnchorPos settings, Action onComplete = null)
            {
                MoveTween?.Kill();
                settings.GetValues(out var endValue, out var duration, out var snapping, out var ease);
                MoveTween = rect
                    .DOAnchorPos(endValue, duration, snapping)
                    .SetEase(ease)
                    .OnComplete(
                        () =>
                        {
                            onComplete?.Invoke();
                        })
                    .OnKill(
                        () =>
                        {
                            MoveTween = null;
                        });
            }
        #endregion
        
        #region Rotate
            private Tween RotateTween;
            
            public void DoRotate(Transform rect, DoRotate settings, Action onUpdate = null, Action onComplete = null)
            {
                RotateTween?.Kill();
                settings.GetValues(out var endValue, out var duration, out var rotateMode, out var ease);
                RotateTween = rect
                    .DORotate(endValue, duration, rotateMode)
                    .SetEase(ease)
                    .OnUpdate(
                        () =>
                        {
                            onUpdate?.Invoke();
                        })
                    .OnComplete(
                        () =>
                        {
                            onComplete?.Invoke();
                        })
                    .OnKill(
                        () =>
                        {
                            RotateTween = null;
                        });
            }
        #endregion
        
        #region Scale
            private Tween ScaleTween;

            public void DoScale(Transform rect, DoScale settings, Action onComplete = null)
            {
                ScaleTween?.Kill();
                settings.GetValues(out var endValue, out var duration, out var ease);
                ScaleTween = rect
                    .DOScale(endValue, duration)
                    .SetEase(ease)
                    .OnComplete(
                        () =>
                        {
                            onComplete?.Invoke();
                        })
                    .OnKill(
                        () =>
                        {
                            ScaleTween = null;
                        });
            }
        #endregion
    }
}