using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.DATA;
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
        
        [field: Header("Battle Card SO")]
        [field: SerializeField] private BattleCardSO BattleCardSO;
        
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

        public Tween TurnToFront()
        {
            RotateTween?.Kill();
            
            var targetAngles = new Vector2(Rect.eulerAngles.x, 180);

            RotateTween = Rect
                .DORotate(targetAngles, .5f, RotateMode.Fast)
                .SetEase(Ease.OutExpo)
                .OnUpdate(() =>
                {
                    var y = Rect.eulerAngles.y;

                    if (Back.activeSelf && y is < 270 and > 90)
                    {
                        Front.SetActive(true);
                        Back.SetActive(false);
                    }
                })
                .OnComplete(() => RotateTween = null)
                .OnKill(() =>
                {
                    Rect.eulerAngles = targetAngles;
                    Front.SetActive(true);
                    Back.SetActive(false);
                });

            return RotateTween;
        }

        public Tween TurnToBack()
        {
            RotateTween?.Kill();
            
            var targetAngles = new Vector2(Rect.eulerAngles.x, 0);

            RotateTween = Rect
                .DORotate(targetAngles, .5f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutExpo)
                .OnUpdate(() =>
                {
                    var y = Rect.eulerAngles.y;

                    if (Front.activeSelf && y is < 360 and > 270 or < 90 and > 0)
                    {
                        Front.SetActive(false);
                        Back.SetActive(true);
                    }
                })
                .OnComplete(() => RotateTween = null)
                .OnKill(() =>
                {
                    Rect.eulerAngles = targetAngles;
                    Front.SetActive(false);
                    Back.SetActive(true);
                });
            
            return RotateTween;
        }
    }
}