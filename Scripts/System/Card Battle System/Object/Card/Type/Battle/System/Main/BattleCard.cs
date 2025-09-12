using System.Card_Battle_System.Object.Card.Base;
using System.Card_Battle_System.Object.Card.Type.Battle.System.Child;
using Data.Card.Battle;
using Data.DOTween.Basic;
using Data.DOTween.Combine;
using DG.Tweening;
using UnityEngine;

namespace System.Card_Battle_System.Object.Card.Type.Battle.System.Main
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCard : CustomPointerEventHandler, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform CardRect;
        [field: SerializeField] private RectTransform CardSurfaceRect;
        
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private BattleCardSO BattleCardData;
        
        [field: Header("Card Order")]
        [field: SerializeField] private Transform CardOrderParent;
        private GameObject CardOrder;
        
        private bool Interactable;
        private bool IsSelected;

        public event Action<ICard> OnClick;
        
        public void GetDrawChance(out float chance)
        {
            chance = BattleCardData.DrawChance;
        }

        #region Card Order
            public void SetCardOrder(GameObject cardOrderPrefab)
            {
                CardOrder = Instantiate(cardOrderPrefab, CardOrderParent);
                AnimationSystem.MoveTo(CardRect, new DoAnchorPos(Vector2.up * 100, .25f, true, Ease.OutExpo));
            }
            
            public void RemoveCardOrder()
            {
                Destroy(CardOrder);
                CardOrder = null;
                AnimationSystem.MoveTo(CardRect, new DoAnchorPos(Vector2.zero, .25f, true, Ease.OutExpo));
            }
        #endregion

        #region CustomPointerEventHandler
            protected override void OnPointerEnter()
            {
                if (!Interactable) return;
                AnimationSystem.ScaleTo(CardSurfaceRect, new DoScale(Vector2.one * 1.25f, .25f, Ease.OutCubic));
            }
            
            protected override void OnPointerExit()
            {
                if (!Interactable) return;
                AnimationSystem.ScaleTo(CardSurfaceRect, new DoScale(Vector2.one, .25f, Ease.OutCubic));
            }
            
            protected override void OnPointerClick()
            {
                if (!Interactable) return;
                IsSelected = !IsSelected;
                OnClick?.Invoke(this);
            }
        #endregion

        #region ICard
            public void SetInteractable(bool interactable)
            {
                Interactable = interactable;
            }

            public void MoveToParent(Transform parent, DoAnchorPos settings, Action onComplete = null)
            {
                CardRect.SetParent(parent);
                AnimationSystem.MoveTo(CardRect, settings)
                    .OnComplete(() => onComplete?.Invoke());
            }

            public void MoveToShowPoint(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null)
            {
                CardRect.SetParent(parent);
                
                flipSettings.GetValues(out var rotateSettings, out var scaleSettings01, out var scaleSettings02);
                
                AnimationSystem.MoveTo(CardRect, anchorPosSettings)
                    .OnComplete(() => AnimationSystem.FlipToFront(CardSurfaceRect, rotateSettings, scaleSettings01, scaleSettings02)
                        .OnComplete(() => onComplete?.Invoke()));
            }
        #endregion
    }
}