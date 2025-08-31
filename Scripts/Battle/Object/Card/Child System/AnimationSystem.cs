using System;
using Data.DOTween_Value;
using DG.Tweening;
using UnityEngine;

namespace Battle.Object.Card.Child_System
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;

        private Tween MainSequence;
        
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        #region Combination Animation
            public void FlipCard(DoFlipValue DoFlipValue, Action OnComplete = null)
            {
                MainSequence?.Kill();
                DoFlipValue.GetValues(out var DoRotateValue, out var DoScaleValue1, out var DoScaleValue2);
                MainSequence = DOTween.Sequence()
                    .Append(RotateTo(DoRotateValue))
                    .Join(ScaleTo(DoScaleValue1, () => ScaleTo(DoScaleValue2)))
                    .OnComplete(() => OnComplete?.Invoke())
                    .OnKill(() => MainSequence = null);
            }
        #endregion
        
        #region Basic Animation
            public Tween MoveTo(DoAnchorPosValue DoAnchorPosValue, Action OnComplete = null)
            {
                MoveTween?.Kill();
                DoAnchorPosValue.GetValues(out var EndValue, out var Duration, out var Snapping, out var Ease);
                MoveTween = Rect
                    .DOAnchorPos(EndValue, Duration, Snapping)
                    .SetEase(Ease)
                    .OnComplete(() =>
                    {
                        Rect.anchoredPosition = EndValue;
                        OnComplete?.Invoke();
                    })
                    .OnKill(() => MoveTween = null);
                return MoveTween;
            }

            public Tween RotateTo(DoRotateValue DoRotateValue, Action OnComplete = null)
            {
                RotateTween?.Kill();
                DoRotateValue.GetValues(out var EndValue, out var Duration, out var RotateMode, out var Ease);
                RotateTween = Rect
                    .DOLocalRotate(EndValue, Duration, RotateMode)
                    .SetEase(Ease)
                    .OnUpdate(() =>
                    {
                        var y = Rect.eulerAngles.y;

                        if (Back.activeSelf && y is < 270 and > 90)
                        {
                            Front.SetActive(true);
                            Back.SetActive(false);
                        }
                        else if (Front.activeSelf && y is < 360 and > 270 or < 90 and > 0)
                        {
                            Front.SetActive(false);
                            Back.SetActive(true);
                        }
                    })
                    .OnComplete(() =>
                    {
                        Rect.eulerAngles = EndValue;
                        OnComplete?.Invoke();
                    })
                    .OnKill(() => RotateTween = null);
                return RotateTween;
            }

            public Tween ScaleTo(DoScaleValue DoScaleValue, Action OnComplete = null)
            {
                ScaleTween?.Kill();
                DoScaleValue.GetValues(out var EndValue, out var Duration, out var Ease);
                ScaleTween = Rect
                    .DOScale(EndValue, Duration)
                    .SetEase(Ease)
                    .OnComplete(() =>
                    {
                        Rect.localScale = EndValue;
                        OnComplete?.Invoke();
                    });
                return ScaleTween;           
            }
        #endregion
    }
}