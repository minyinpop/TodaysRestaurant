using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OBJECT.BATTLE.INITIATIVE_BATTLE_COIN
{
    internal class InitiativeBattleCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private RectTransform Rect;
        
        [field: SerializeField] private GameObject Heads;
        [field: SerializeField] private GameObject Tails;
        
        private RectTransform TossPoint;
        private RectTransform LandPoint;
        private RectTransform ShowPoint;

        private bool Interactable;

        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        public void Initialize(RectTransform tossPoint, RectTransform landPoint, RectTransform showPoint)
        {
            Rect.anchoredPosition3D = Vector3.zero;
            
            TossPoint = tossPoint;
            LandPoint = landPoint;
            ShowPoint = showPoint;
        }
        
        public void MoveCoinToTossPoint()
        {
            KillTween();
            Rect.SetParent(TossPoint);
            MoveTween = MoveCoinOnTossPoint();
        }
        
        #region Set Interactable
            private void TurnOnInteract() => Interactable = true;
            private void TurnOffInteract() => Interactable = false;
        #endregion
        
        #region Pointer Event
            public void OnPointerEnter(PointerEventData eventData)
            {
                if (!Interactable) return;
                KillTween();
                ScaleTween = ScaleOnCursorEnter();
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                if (!Interactable) return;
                KillTween();
                ScaleTween = ScaleOnCursorExit();
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (!Interactable) return;
                TurnOffInteract();
                
                KillTween();
                DOTween.Sequence()
                    .AppendCallback(() => Rect.SetParent(LandPoint))
                    .Append(ThrowCoin())
                    .AppendInterval(.5f)
                    .AppendCallback(() => Rect.SetParent(ShowPoint))
                    .Append(ShowCoin())
                    .OnComplete(() => { Debug.Log("Show Finish."); });
            }
        #endregion
        
        #region Pointer Enter & Exit Tween
        private Tween ScaleOnCursorEnter()
        {
            return Rect
                .DOScale(Vector2.one * 1.25f, 1)
                .SetEase(Ease.OutExpo);
        }

        private Tween ScaleOnCursorExit()
        {
            return Rect
                .DOScale(Vector2.one, 1)
                .SetEase(Ease.OutExpo);
        }
        #endregion
        
        #region Main Tween
            private Tween ThrowCoin()
            {
                return DOTween.Sequence()
                    .Append(MoveTween = MoveToRandomPointOnLandPoint())
                    .Join(RotateTween = RotateRandomTurns())
                    .Join(ScaleTween = SimulateThrowScale());
            }

            private Tween ShowCoin()
            {
                return DOTween.Sequence()
                    .Append(MoveTween = MoveCoinOnShowPoint())
                    .Append(ScaleTween = ScaleCoinOnShowPoint());
            }
        #endregion
            
        #region Toss Point Tween
            private Tween MoveCoinOnTossPoint()
            {
                return Rect
                    .DOAnchorPos(Vector2.zero, 1, true)
                    .SetEase(Ease.OutExpo)
                    .OnComplete(TurnOnInteract);
            }
        #endregion
        
        #region Land Point Tween
            private Tween MoveToRandomPointOnLandPoint()
            {
                var randomXMultiply = Random.Range(.2f, .8f);
                var randomYMultiply = Random.Range(.5f, .8f);
                var randomX = 1920 * randomXMultiply;
                var randomY = 1080 * randomYMultiply;
                var randomPoint = new Vector2(randomX, randomY);

                return Rect
                    .DOAnchorPos(randomPoint, 3, true)
                    .SetEase(Ease.Linear);
            }

            private Tween RotateRandomTurns()
            {
                var randomYTurns = Random.Range(24, 32);
                var randomZTurns = Random.Range(16, 26);
                var randomYAngle = 180 * randomYTurns;
                var randomZAngle = Random.Range(1, 361) * randomZTurns;
                var randomRotate = new Vector3(Rect.eulerAngles.x, randomYAngle, randomZAngle);

                return Rect
                    .DORotate(randomRotate, 3, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .OnUpdate(() =>
                    {
                        var y = Rect.eulerAngles.y;

                        if (Heads.activeSelf && y is > 90 and < 270)
                        {
                            Heads.SetActive(false);
                            Tails.SetActive(true);
                        }
                        else if (Tails.activeSelf && y is > 270 and < 360 or > 0 and < 90)
                        {
                            Heads.SetActive(true);
                            Tails.SetActive(false);
                        }
                    });
            }

            private Tween SimulateThrowScale()
            {
                return DOTween.Sequence()
                    .Append(Rect
                        .DOScale(Vector2.one * 1.5f, 1.5f)
                        .SetEase(Ease.OutSine))
                    .Append(Rect
                        .DOScale(Vector2.one, 1.5f)
                        .SetEase(Ease.InSine));
            }
        #endregion
            
        #region Show Point Tween
            private Tween MoveCoinOnShowPoint()
            {
                return Rect
                    .DOAnchorPos(Vector2.zero, 1, true)
                    .SetEase(Ease.OutExpo);
            }
            
            private Tween ScaleCoinOnShowPoint()
            {
                return Rect
                    .DOScale(Vector2.one * 2, .5f)
                    .SetEase(Ease.InOutBack);
            }
        #endregion

        #region Tween Tool
            private void KillTween()
            {
                MoveTween?.Kill();
                RotateTween?.Kill();
                ScaleTween?.Kill();
                
                MoveTween = null;
                RotateTween = null;
                ScaleTween = null;
            }
        #endregion
    }
}