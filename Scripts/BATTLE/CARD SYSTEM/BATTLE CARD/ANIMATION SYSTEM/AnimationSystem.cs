using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.BATTLE_CARD.ANIMATION_SYSTEM
{
    internal class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;

        public Tween ScaleUpWhenCursorEnter()
        {
            ScaleTween?.Kill();

            ScaleTween = Rect
                .DOScale(Vector2.one * 1.25f, .5f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => ScaleTween = null);
            
            return ScaleTween;
        }

        public Tween ScaleDownWhenCursorExit()
        {
            ScaleTween?.Kill();
            
            ScaleTween = Rect
                .DOScale(Vector2.one, .5f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => ScaleTween = null);
            
            return ScaleTween;
        }

        public Tween PopUpWhenCursorClick()
        {
            MoveTween?.Kill();

            var targetPos = Rect.anchoredPosition + Vector2.up * 150;

            MoveTween = Rect
                .DOAnchorPos(targetPos, .5f, true)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => MoveTween = null)
                .OnKill(() => Rect.anchoredPosition = targetPos);

            return MoveTween;
        }
        
        public Tween PopDownWhenCursorClick()
        {
            MoveTween?.Kill();

            var targetPos = Rect.anchoredPosition + Vector2.down * 150;

            MoveTween = Rect
                .DOAnchorPos(targetPos, .2f, true)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => MoveTween = null)
                .OnKill(() => Rect.anchoredPosition = targetPos);

            return MoveTween;
        }
    }
}