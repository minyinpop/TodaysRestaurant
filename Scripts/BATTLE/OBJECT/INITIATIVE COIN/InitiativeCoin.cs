using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.OBJECT.INITIATIVE_COIN
{
    internal class InitiativeCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private RectTransform Rect;
        
        [field: SerializeField] private GameObject Heads;
        [field: SerializeField] private GameObject Tails;

        private Tween Tween;
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;

        private bool Interactable;
        
        #region Pointer Events
            public void OnPointerEnter(PointerEventData eventData)
            {
                if (!Interactable) return;
                
                KillTween();
                
                ScaleTween = Rect
                    .DOScale(Vector2.one * 1.25f, .3f)
                    .SetEase(Ease.OutQuad);
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                if (!Interactable) return;
                
                KillTween();
                
                ScaleTween = Rect
                    .DOScale(Vector2.one, .3f)
                    .SetEase(Ease.OutQuad);
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (!Interactable) return;
            }
        #endregion
        
        public void MoveCoinToTossPoint(RectTransform tossPoint)
        {
            KillTween();

            Tween = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    Rect.SetParent(tossPoint);
                })
                .Append(MoveTween = Rect
                    .DOAnchorPos(tossPoint.anchoredPosition, 1, true)
                    .SetEase(Ease.OutBack))
                .OnComplete(() =>
                {
                    Interactable = true;
                });
        }

        public void MoveCoinToShowPoint(RectTransform showPoint)
        {
            KillTween();
            // TODO
        }

        private void KillTween()
        {
            Tween?.Kill();
            MoveTween?.Kill();
            RotateTween?.Kill();
            ScaleTween?.Kill();

            Tween = null;
            MoveTween = null;
            RotateTween = null;
            ScaleTween = null;
        }
    }
}