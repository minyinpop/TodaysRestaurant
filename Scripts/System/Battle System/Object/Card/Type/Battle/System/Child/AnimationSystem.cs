using Data.Animation.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Battle_System.Object.Card.Type.Battle.System.Child
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;

        private Tween CurrentSequence;
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        #region Combine Animation
            public Tween FlipToFront(RectTransform rect, DoRotate rotateSettings, DoScale scaleSettings01, DoScale scaleSettings02)
            {
                CurrentSequence?.Kill();

                CurrentSequence = DOTween.Sequence()
                    .Append(RotateTo(rect, rotateSettings))
                    .Join(ScaleTo(rect, scaleSettings01)
                        .OnComplete(() => ScaleTo(rect, scaleSettings02)))
                    .OnKill(() => CurrentSequence = null);
                
                return CurrentSequence;
            }
        #endregion

        #region Basic Animation
            public Tween MoveTo(RectTransform rect, DoAnchorPos settings)
            {
                MoveTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out var snapping, out var ease);
                
                MoveTween = rect
                    .DOAnchorPos(endValue, duration, snapping)
                    .SetEase(ease)
                    .OnComplete(() => rect.anchoredPosition = endValue)
                    .OnKill(() => MoveTween = null);

                return MoveTween;
            }
            
            public Tween RotateTo(RectTransform rect, DoRotate settings)
            {
                RotateTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out var rotateMode, out var ease);

                RotateTween = rect
                    .DORotate(endValue, duration, rotateMode)
                    .SetEase(ease)
                    .OnUpdate(() =>
                    {
                        var y = rect.eulerAngles.y;

                        if (Front.activeSelf && y is < 360 and > 270 or < 90 and > 0)
                        {
                            Front.SetActive(false);
                            Back.SetActive(true);
                        }
                        else if (Back.activeSelf && y is < 270 and > 90)
                        {
                            Front.SetActive(true);
                            Back.SetActive(false);
                        }
                    })
                    .OnComplete(() => rect.eulerAngles = endValue)
                    .OnKill(() => RotateTween = null);
                
                return RotateTween;
            }
            
            public Tween ScaleTo(RectTransform rect, DoScale settings)
            {
                ScaleTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out var ease);
                
                ScaleTween = rect
                    .DOScale(endValue, duration)
                    .SetEase(ease)
                    .OnComplete(() => rect.localScale = endValue)
                    .OnKill(() => ScaleTween = null);
                
                return ScaleTween;
            }
        #endregion
    }
}