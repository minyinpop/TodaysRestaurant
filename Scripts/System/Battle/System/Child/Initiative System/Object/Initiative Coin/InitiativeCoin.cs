using System.General;
using Data.Animation.DOTween.Basic;
using Data.Animation.DOTween.Combine;
using Data.General.Enum;
using DG.Tweening;
using General;
using UnityEngine;

namespace System.Battle.System.Child.Initiative_System.Object.Initiative_Coin
{
    [RequireComponent(typeof(AnimationSystem))]
    [RequireComponent(typeof(PointerEvent))]
    internal sealed class InitiativeCoin : PointerEvent
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private PointerEvent pointerEvent;

        [field: Header("Parent")]
        [field: SerializeField] private RectTransform ReadyParent;
        [field: SerializeField] private RectTransform ThrowParent;
        [field: SerializeField] private RectTransform ShowParent;

        private bool Interactable;

        // Default is Tails
        private TossResult TossResult = TossResult.Tails;

        public event Action<TossResult> OnShowTossResult;
        
        public void MoveToReadyParent()
        {
            Rect.SetParent(ReadyParent);
            AnimationSystem.MoveTo(new DoAnchorPos(Vector2.zero, 1, true, Ease.OutBack))
                .OnComplete(() => Interactable = true);
        }

        private void MoveToThrowParent()
        {
            Rect.SetParent(ThrowParent);

            // Position
            var randomXPos = 1920 * UnityEngine.Random.Range(.2f, .8f);
            var randomYPos = 1080 * UnityEngine.Random.Range(.5f, .8f);
                
            var finalPos = new Vector2(randomXPos, randomYPos);

            // Angle Y
            var randomYTurns = UnityEngine.Random.Range(24, 32);
            var finalYAngle = 180 * randomYTurns;
            
            // Angle Z
            var randomZTurns = UnityEngine.Random.Range(12, 24);
            var randomZAngle = UnityEngine.Random.Range(1, 360);
            var finalZAngle = 360 * randomZTurns + randomZAngle;
                
            var finalAngle = new Vector3(Rect.eulerAngles.x, finalYAngle, finalZAngle);
                
            const float throwDuration = 3f;
                
            var anchorPosSettings = new DoAnchorPos(finalPos, throwDuration, true, Ease.Linear);
            var rotateSettings = new DoRotate(finalAngle, throwDuration, RotateMode.FastBeyond360, Ease.Linear);
            var scaleSettings01 = new DoScale(Vector2.one * 1.5f, throwDuration / 2, Ease.OutSine);
            var scaleSettings02 = new DoScale(Vector2.one, throwDuration / 2, Ease.InSine);
            
            var throwSettings = new DoThrow(anchorPosSettings, rotateSettings, scaleSettings01, scaleSettings02);
            
            const float callbackDelay = 1;
            
            TossResult = randomYTurns % 2 == 0 ? TossResult.Tails : TossResult.Heads;

            AnimationSystem.ThrowTo(throwSettings, callbackDelay)
                .OnComplete(MoveToShowParent);
        }

        private void MoveToShowParent()
        {
            Rect.SetParent(ShowParent);

            var anchorPosSettings = new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutCubic);
            var scaleSettings = new DoScale(Vector2.one * 2, 1, Ease.InOutBack);
            
            const float callbackDelay = 1;

            AnimationSystem.ShowCoin(anchorPosSettings, scaleSettings, callbackDelay)
                .OnComplete(() => OnShowTossResult?.Invoke(TossResult));
        }

        public void Hide(float hideDuration, Action onComplete = null)
        {
            AnimationSystem.ScaleTo(new DoScale(Vector2.zero, hideDuration, Ease.InBack))
                .OnComplete(() => onComplete?.Invoke());
        }

        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!Interactable) return;
                AnimationSystem.ScaleTo(new(Vector2.one * 1.25f, .25f, Ease.OutCubic));
            }
            
            protected override void OnPointerExit()
            {
                if (!Interactable) return;
                AnimationSystem.ScaleTo(new(Vector2.one, .25f, Ease.OutCubic));
            }

            protected override void OnPointerClick()
            {
                if (!Interactable) return;
                Interactable = false;
                MoveToThrowParent();
            }
        #endregion
    }
}