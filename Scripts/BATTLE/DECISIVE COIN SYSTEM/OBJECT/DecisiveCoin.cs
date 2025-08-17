using BATTLE.DECISIVE_COIN_SYSTEM.Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.DECISIVE_COIN_SYSTEM.OBJECT
{
    internal class DecisiveCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;

        [field: Header("Side")]
        [field: SerializeField] private GameObject Heads;
        [field: SerializeField] private GameObject Tails;
        
        [field: Header("Target Point")]
        [field: SerializeField] private RectTransform ShowPoint;
        [field: SerializeField] private RectTransform LandPoint;
        [field: SerializeField] private RectTransform TossPoint;
        [field: SerializeField] private RectTransform PreparePoint;
        
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;

        private bool Interactable;

        public event System.Action OnClicked;
        
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
            Interactable = false;
            OnClicked?.Invoke();
        }
        
        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;
        }
        
        public Tween MoveToTossPoint()
        {
            KillTween();
            
            Rect.SetParent(TossPoint);
            MoveTween = MoveToZero();
            return MoveTween;
        }

        public Tween Tossing(out TossResult result)
        {
            KillTween();
            
            Rect.SetParent(LandPoint);
            return PlayAnimationWhenToss(out result);
        }

        public Tween MoveToShowPoint()
        {
            KillTween();
            
            Rect.SetParent(ShowPoint);
            MoveTween = ShowTossResult();
            return MoveTween;
        }

        public Tween HideCoin()
        {
            KillTween();
            ScaleTween = ScaleCoinToZeroWhenHide();
            return ScaleTween;
        }
        
        #region Tween
            private Tween MoveToZero(float duration = 1)
            {
                return Rect
                    .DOAnchorPos(Vector2.zero, duration, true)
                    .SetEase(Ease.OutSine);
            }
            
            
            
            
            
            private Tween ScaleOnCursorEnter()
            {
                return Rect
                    .DOScale(Vector2.one * 1.25f, .25f)
                    .SetEase(Ease.OutExpo);
            }
            
            private Tween ScaleOnCursorExit()
            {
                return Rect
                    .DOScale(Vector2.one, .25f)
                    .SetEase(Ease.OutExpo);
            }





            private Tween PlayAnimationWhenToss(out TossResult result)
            {
                return DOTween.Sequence()
                    .Append(ScaleTween = SetScaleToOneWhenClicked())
                    .Append(MoveTween = MoveCoinWhenClicked())
                    .Join(RotateTween = TurnCoinWhenClicked(out result))
                    .Join(ScaleTween = ScaleCoinWhenClicked())
                    .AppendInterval(1)
                    .OnUpdate(ChangeSideWhenToss());
            }
            
            private Tween SetScaleToOneWhenClicked()
            {
                return Rect
                    .DOScale(Vector2.one, .25f)
                    .SetEase(Ease.Linear);
            }

            private Tween MoveCoinWhenClicked()
            {
                var randomXMult = Random.Range(.2f, .8f);
                var randomYMult = Random.Range(.65f, .8f);
                var randomX = 1920 * randomXMult;
                var randomY = 1080 * randomYMult;
                var targetPoint = new Vector2(randomX, randomY);

                return Rect
                    .DOAnchorPos(targetPoint, 2, true)
                    .SetEase(Ease.Linear);
            }

            private Tween TurnCoinWhenClicked(out TossResult result)
            {
                var randomYTurns = Random.Range(24, 32);
                var randomZTurns = Random.Range(24, 32);
                var randomYAngle = 180 * randomYTurns;
                var randomZAngle = Random.Range(1, 361) * randomZTurns;
                var targetAngle = new Vector3(Rect.eulerAngles.x, randomYAngle, randomZAngle);

                result = randomYTurns % 2 == 0 ? TossResult.Heads : TossResult.Tails;
                
                return Rect
                    .DORotate(targetAngle, 2, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear);
            }

            private Tween ScaleCoinWhenClicked()
            {
                return DOTween.Sequence()
                    .Append(Rect
                        .DOScale(Vector2.one * 2, 1)
                        .SetEase(Ease.OutCubic))
                    .Append(Rect
                        .DOScale(Vector2.one, 1)
                        .SetEase(Ease.InCubic));
            }

            private TweenCallback ChangeSideWhenToss()
            {
                return () =>
                {
                    var y = Rect.eulerAngles.y;

                    if (Heads.activeSelf && y is < 270 and > 90)
                    {
                        Heads.SetActive(false);
                        Tails.SetActive(true);
                    }
                    else if (Tails.activeSelf && y is < 360 and > 270 or < 90 and > 0)
                    {
                        Heads.SetActive(true);
                        Tails.SetActive(false);
                    }
                };
            }
            
            
            
            
            
            private Tween ShowTossResult(System.Action onComplete = null)
            {
                return DOTween.Sequence()
                    .Append(MoveTween = MoveToZero())
                    .Append(ScaleTween = ScaleCoinWhenShowTossResult())
                    .OnComplete(() => onComplete?.Invoke());
            }

            private Tween ScaleCoinWhenShowTossResult()
            {
                return Rect
                    .DOScale(Vector2.one * 2, 1)
                    .SetEase(Ease.OutBack);
            }
            
            
            
            
            
            private Tween ScaleCoinToZeroWhenHide(System.Action onComplete = null)
            {
                return Rect
                    .DOScale(Vector2.zero, 1)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => onComplete?.Invoke());
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
        #endregion
    }
}