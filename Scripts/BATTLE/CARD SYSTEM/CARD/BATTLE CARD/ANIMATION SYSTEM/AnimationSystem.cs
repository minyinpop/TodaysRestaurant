using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.ANIMATION_SYSTEM
{
    internal class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        private Tween MoveTween;
        
        public Tween MoveToCardPoolSlotWhenSpawn()
        {
            MoveTween?.Kill();

            MoveTween = Rect
                .DOAnchorPos(Vector2.zero, 1, true)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => Rect.anchoredPosition = Vector2.zero)
                .OnKill(() => MoveTween = null);

            return MoveTween;
        }
    }
}