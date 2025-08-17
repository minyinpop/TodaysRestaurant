using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.ITEM_CARD.ANIMATION_SYSTEM
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
    }
}