using DG.Tweening;
using UnityEngine;

namespace Battle_Management_System.Card_System.Battle_Card
{
    internal class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Object")]
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;

        private Sequence CurrentSequence;
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        public Tween MoveCardToVectorZero(float duration = 1)
        {
            MoveTween?.Kill();

            MoveTween = Rect
                .DOAnchorPos(Vector2.zero, duration, true)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => Rect.anchoredPosition = Vector2.zero)
                .OnKill(() => MoveTween = null);
            
            return MoveTween;
        }

        private Tween RotateCardToFront(float duration = 1)
        {
            RotateTween?.Kill();
            
            RotateTween = Rect
                .DORotate(Vector2.up * 180, duration, RotateMode.Fast)
                .SetEase(Ease.Linear)
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
                .OnComplete(() => Rect.eulerAngles = Vector2.up * 180)
                .OnKill(() => RotateTween = null);

            return RotateTween;
        }

        private Tween ScaleUpAndDown(float duration = 1, float scaleSize = 1.25f)
        {
            ScaleTween?.Kill();
            
            ScaleTween = DOTween.Sequence()
                .Append(Rect
                    .DOScale(Vector2.one * scaleSize, duration / 2)
                    .SetEase(Ease.OutSine))
                .Append(Rect
                    .DOScale(Vector2.one, duration / 2)
                    .SetEase(Ease.InSine))
                .OnComplete(() => Rect.localScale = Vector2.one)
                .OnKill(() => ScaleTween = null);
            
            return ScaleTween;
        }

        public Tween MoveCardToSlotAndFlip(float moveDuration = 1, float flipDuration = .5f, float scaleDuration = .5f, float scaleSize = 1.25f)
        {
            CurrentSequence?.Kill();
            MoveTween?.Kill();
            RotateTween?.Kill();
            ScaleTween?.Kill();

            CurrentSequence = DOTween.Sequence()
                .Append(MoveCardToVectorZero(moveDuration))
                .Append(RotateCardToFront(flipDuration))
                .Join(ScaleUpAndDown(scaleDuration, scaleSize))
                .OnComplete(() =>
                {
                    CurrentSequence = null;
                    MoveTween = null;
                    RotateTween = null;
                    ScaleTween = null;
                });
            
            return CurrentSequence;
        }
    }
}