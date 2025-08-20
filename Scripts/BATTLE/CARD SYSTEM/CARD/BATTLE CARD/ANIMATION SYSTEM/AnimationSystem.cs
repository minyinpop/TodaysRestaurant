using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.ANIMATION_SYSTEM
{
    internal class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;
        
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
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

        #region Move To Show Point When Draw
            public Tween MoveToShowPointWhenDraw()
            {
                MoveTween?.Kill();

                return DOTween.Sequence()
                    .Append(MoveToZeroAtShowPointWhenDraw())
                    .Append(RotateToFrontAtShowPointWhenDraw())
                    .Join(ScaleUpAndDownWhenDraw());
            }

            private Tween MoveToZeroAtShowPointWhenDraw()
            {
                MoveTween?.Kill();
                
                MoveTween = Rect
                    .DOAnchorPos(Vector2.zero, 1, true)
                    .SetEase(Ease.OutExpo)
                    .OnComplete(() => Rect.anchoredPosition = Vector2.zero)
                    .OnKill(() => MoveTween = null);
                
                return MoveTween;
            }
            
            private Tween RotateToFrontAtShowPointWhenDraw()
            {
                RotateTween?.Kill();

                RotateTween = Rect
                    .DORotate(Vector2.up * 180, 1, RotateMode.Fast)
                    .SetEase(Ease.Linear)
                    .OnUpdate(() =>
                    {
                        var y = Rect.eulerAngles.y;

                        if (Back.activeSelf && y is < 270 and > 90)
                        {
                            Front.SetActive(true);
                            Back.SetActive(false);
                        }
                    })
                    .OnComplete(() => Rect.eulerAngles = new Vector2(Rect.eulerAngles.x, 180))
                    .OnKill(() => RotateTween = null);

                return RotateTween;
            }

            private Tween ScaleUpAndDownWhenDraw()
            {
                ScaleTween?.Kill();
                
                ScaleTween = DOTween.Sequence()
                    .Append(ScaleUpWhenDraw())
                    .Append(ScaleDownWhenDraw())
                    .OnComplete(() => ScaleTween = null)
                    .OnKill(() => ScaleTween = null);
                
                return ScaleTween;
            }

            private Tween ScaleUpWhenDraw()
            {
                return Rect
                    .DOScale(Vector2.one * 1.25f, .5f)
                    .SetEase(Ease.Linear);
            }
            
            private Tween ScaleDownWhenDraw()
            {
                return Rect
                    .DOScale(Vector2.one, .5f)
                    .SetEase(Ease.Linear);
            }
        #endregion
    }
}