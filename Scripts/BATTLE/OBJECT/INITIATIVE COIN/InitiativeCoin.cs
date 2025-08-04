using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        private CanvasScaler MainCanvasScaler;
        private RectTransform ReadyPoint;
        private RectTransform TossPoint;
        private RectTransform ShowPoint;

        private bool Interactable;

        public event System.Action OnTossComplete;
        
        #region Pointer Events
            public void OnPointerEnter(PointerEventData eventData)
            {
                if (!Interactable) return;
                KillTween();
                ScaleTween = OnCursorEnter();
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                if (!Interactable) return;
                KillTween();
                ScaleTween = OnCursorExit();
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (!Interactable) return;
                Interactable = false;

                KillTween();
                
                Rect.SetParent(TossPoint);

                DOTween.Sequence()
                    .Append(ScaleTween = OnCoinToss())
                    .Append(MoveTween = ChooseRandomTossLandingPoint())
                    .Join(RotateTween = ChooseRandomRotateAngles())
                    .Join(ScaleTween = OnCoinLanding())
                    .OnUpdate(ChangeSideWhenRotate)
                    .OnComplete(()=>{ OnTossComplete?.Invoke(); });
            }
        #endregion

        public void Initialization(CanvasScaler main, RectTransform ready, RectTransform toss, RectTransform show)
        {
            MainCanvasScaler = main;
            ReadyPoint = ready;
            TossPoint = toss;
            ShowPoint = show;
        }
        
        public void MoveCoinToReadyPoint()
        {
            KillTween();
            Rect.SetParent(ReadyPoint);
            MoveTween = CoinMoveToReadyPoint()
                .OnComplete(() => { Interactable = true; });
        }

        public void MoveCoinToShowPoint()
        {
            KillTween();
            // TODO Show the result about toss.
        }

        private Tween OnCursorEnter()
        {
            return Rect
                .DOScale(Vector2.one * 1.25f, .25f)
                .SetEase(Ease.OutQuad);
        }

        private Tween OnCursorExit()
        {
            return Rect
                .DOScale(Vector2.one, .25f)
                .SetEase(Ease.OutQuad);
        }

        private Tween CoinMoveToReadyPoint()
        {
            return Rect
                .DOAnchorPos(ReadyPoint.anchoredPosition, 1, true)
                .SetEase(Ease.OutBack);
        }

        private Tween ChooseRandomTossLandingPoint()
        {
            var randomXMult = Random.Range(.25f, .75f);
            var randomYMult = Random.Range(.65f, .8f);
            var randomX = MainCanvasScaler.referenceResolution.x * randomXMult;
            var randomY = MainCanvasScaler.referenceResolution.y * randomYMult;
            return Rect
                .DOAnchorPos(new Vector2(randomX, randomY), 2, true)
                .SetEase(Ease.InBack);
        }

        private Tween ChooseRandomRotateAngles()
        {
            var randomYRotTurns = Random.Range(8, 13);
            var randomZRotTurns = Random.Range(8, 13);
            var randomYAngle = 180 * randomYRotTurns;
            var randomZAngle = Random.Range(0, 361) * randomZRotTurns;
            return Rect
                .DOLocalRotate(new Vector3(transform.eulerAngles.x, randomYAngle, randomZAngle), 2, RotateMode.FastBeyond360)
                .SetEase(Ease.InQuad);
        }

        private Tween OnCoinToss()
        {
            return Rect
                .DOScale(Vector2.one * 1.25f, 1)
                .SetEase(Ease.OutQuad);
        }

        private Tween OnCoinLanding()
        {
            return Rect
                .DOScale(Vector2.one, 2)
                .SetEase(Ease.OutSine);
        }

        private void ChangeSideWhenRotate()
        {
            if (Heads.activeSelf && transform.eulerAngles.y is >= 90 and < 270)
            {
                Heads.SetActive(false);
                Tails.SetActive(true);
            }
            else if (Tails.activeSelf && transform.eulerAngles.y is >= 270 and <= 360 or >= 0 and < 90)
            {
                Heads.SetActive(true);
                Tails.SetActive(false);
            }
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