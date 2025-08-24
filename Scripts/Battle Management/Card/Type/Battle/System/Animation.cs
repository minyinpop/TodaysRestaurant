using DG.Tweening;
using UnityEngine;

namespace Battle_Management.Card.Type.Battle.System
{
    internal class Animation : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;

        private Tween MoveTween;

        public Tween MoveToZero(float duration = 1f)
        {
            MoveTween?.Kill();

            MoveTween = Rect
                .DOAnchorPos(Vector2.zero, duration, true)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => Rect.anchoredPosition = Vector2.zero)
                .OnKill(() => MoveTween = null);
            
            return MoveTween;
        }
    }
}