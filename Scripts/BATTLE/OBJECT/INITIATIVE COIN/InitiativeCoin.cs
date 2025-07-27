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

        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        private RectTransform ReadyPoint;
        private RectTransform TossPoint;
        private RectTransform ShowPoint;

        private bool Interactable;
        
        #region Pointer Events
            public void OnPointerEnter(PointerEventData eventData)
            {
                if (!Interactable) return;
                
                KillTween();
                
                ScaleTween = Rect
                    .DOScale(Vector2.one * 1.25f, .25f)
                    .SetEase(Ease.OutQuad);
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                if (!Interactable) return;
                
                KillTween();
                
                ScaleTween = Rect
                    .DOScale(Vector2.one, .25f)
                    .SetEase(Ease.OutQuad);
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (!Interactable) return;
                Interactable = false;

                KillTween();
                
                Rect.SetParent(TossPoint);
                
                var testVector2 = TossPoint.anchoredPosition;

                DOTween.Sequence()
                    .Append(MoveTween = Rect
                        .DOAnchorPos(testVector2, 1, true)
                        .SetEase(Ease.OutQuad));
                
                // TODO 目前點擊硬幣後，它會到螢幕中心點位置
            }
        #endregion

        public void Initialization(RectTransform ready, RectTransform toss, RectTransform show)
        {
            ReadyPoint = ready;
            TossPoint = toss;
            ShowPoint = show;
        }
        
        public void MoveCoinToReadyPoint()
        {
            KillTween();

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    Rect.SetParent(ReadyPoint);
                })
                .Append(MoveTween = Rect
                    .DOAnchorPos(ReadyPoint.anchoredPosition, 1, true)
                    .SetEase(Ease.OutBack))
                .OnComplete(() =>
                {
                    Interactable = true;
                });
        }

        public void MoveCoinToShowPoint()
        {
            KillTween();
            // TODO
        }

        private void KillTween()
        {
            MoveTween?.Kill();
            RotateTween?.Kill();
            ScaleTween?.Kill();

            MoveTween = null;
            RotateTween = null;
            ScaleTween = null;
        }
    }
}