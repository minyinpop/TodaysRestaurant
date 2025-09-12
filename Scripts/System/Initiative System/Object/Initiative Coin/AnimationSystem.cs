using Data.DOTween.Basic;
using Data.DOTween.Combine;
using DG.Tweening;
using UnityEngine;

namespace System.Initiative_System.Object.Initiative_Coin
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Object")]
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;

        private Tween CurrentSequence;
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;

        #region Combine Animation
            public Tween ThrowTo(DoThrow settings, float callbackDelay)
            {
                CurrentSequence?.Kill();
                
                settings.GetValues(out var anchorPosSettings, out var rotateSettings, out var scaleSettings01, out var scaleSettings02);

                CurrentSequence = DOTween.Sequence()
                    .Append(MoveTo(anchorPosSettings))
                    .Join(RotateTo(rotateSettings))
                    .Join(ScaleTo(scaleSettings01)
                        .OnComplete(() => ScaleTo(scaleSettings02)))
                    .AppendInterval(callbackDelay)
                    .OnKill(() => CurrentSequence = null);

                return CurrentSequence;
            }

            public Tween ShowCoin(DoAnchorPos anchorPosSettings, DoScale scaleSettings, float callbackDelay)
            {
                CurrentSequence?.Kill();

                CurrentSequence = DOTween.Sequence()
                    .Append(MoveTo(anchorPosSettings))
                    .Append(ScaleTo(scaleSettings))
                    .AppendInterval(callbackDelay)
                    .OnKill(() => CurrentSequence = null);
                
                return CurrentSequence;
            }
        #endregion

        #region Basic Animation
            public Tween MoveTo(DoAnchorPos settings)
            {
                MoveTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out var snapping, out var ease);
                
                MoveTween = Rect
                    .DOAnchorPos(endValue, duration, snapping)
                    .SetEase(ease)
                    .OnComplete(() => Rect.anchoredPosition = endValue)
                    .OnKill(() => MoveTween = null);
                
                return MoveTween;
            }

            public Tween RotateTo(DoRotate settings)
            {
                RotateTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out RotateMode rotateMode, out var ease);
                
                RotateTween = Rect
                    .DORotate(endValue, duration, rotateMode)
                    .SetEase(ease)
                    .OnUpdate(() =>
                    {
                        var y = Rect.localEulerAngles.y;

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
                    .OnComplete(() => Rect.localEulerAngles = endValue)
                    .OnKill(() => RotateTween = null);
                
                return RotateTween;
            }

            public Tween ScaleTo(DoScale settings)
            {
                ScaleTween?.Kill();
                
                settings.GetValues(out var endValue, out float duration, out var ease);
                
                ScaleTween = Rect
                    .DOScale(endValue, duration)
                    .SetEase(ease)
                    .OnComplete(() => Rect.localScale = endValue)
                    .OnKill(() => ScaleTween = null);
                
                return ScaleTween;
            }
        #endregion
    }
}