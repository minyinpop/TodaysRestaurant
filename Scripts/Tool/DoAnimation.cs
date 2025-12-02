using System;
using Data.Animation.DOTween.Basic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tool
{
    internal sealed class DoAnimation : MonoBehaviour
    {
        private void OnEnable()
        {
            DOTween.useSafeMode = true;
        }

        private void OnDisable()
        {
            MoveTween?.Kill();
            RotateTween?.Kill();
            ScaleTween?.Kill();
            DoFade_CanvasGroup_Tween?.Kill();
            DoValue_Slider_Tween?.Kill();
        }

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
            
            public void DoScale_UI(RectTransform rect, DoScale settings, Action onComplete = null)
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

            public void DoScale_WorldSpace(Transform trans, DoScale settings, Action onComplete = null)
            {
                ScaleTween?.Kill();
                settings.GetValues(out var endValue, out var duration, out var ease);
                ScaleTween = trans
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
        
        #region Fade
            private Tween DoFade_CanvasGroup_Tween;
            
            public void DoFade_CanvasGroup(CanvasGroup canvasGroup, DoFade_CanvasGroup settings, Action onComplete = null)
            {
                DoFade_CanvasGroup_Tween?.Kill();
                settings.GetValues(out var endValue, out var duration, out var ease);
                DoFade_CanvasGroup_Tween = canvasGroup
                    .DOFade(endValue, duration)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    })
                    .OnKill(() =>
                    {
                        DoFade_CanvasGroup_Tween = null;
                    });
            }
        #endregion
        
        #region Value
            private Tween DoValue_Slider_Tween;

            public void DoValue_Slider(Slider slider, DoValue_Slider settings, Action onComplete = null)
            {
                DoValue_Slider_Tween?.Kill();
                settings.GetValues(out var endValue, out var duration, out var snapping, out var ease);
                DoValue_Slider_Tween = slider
                    .DOValue(endValue, duration, snapping)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    })
                    .OnKill(() =>
                    {
                        DoValue_Slider_Tween = null;
                    });
            }
        #endregion
    }
}