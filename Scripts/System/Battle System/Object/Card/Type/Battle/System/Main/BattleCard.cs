using System.Battle_System.Object.Card.Base;
using System.Battle_System.Object.Card.Type.Battle.System.Child;
using Data.Animation.DOTween.Basic;
using Data.Animation.DOTween.Combine;
using Data.Animation.Spine;
using Data.Card.Battle;
using Data.General;
using Data.General.Damage.Base;
using DG.Tweening;
using UnityEngine;

namespace System.Battle_System.Object.Card.Type.Battle.System.Main
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
        [field: SerializeField] protected BattleCardSO BattleCardData;
        
        [field: Header("Card Order")]
        [field: SerializeField] private Transform CardOrderParent;
        private GameObject CardOrder;
        
        private bool Interactable;

        public event Action<ICard> OnClick;
        public static event Action<BattleCard, SkeletonAnimationSettings, Action> OnUse;
        
        public void GetDrawChance(out float chance)
        {
            BattleCardData.GetDrawChance(out chance);
        }

        public void GetDamage(out Damage damage)
        {
            BattleCardData.GetDamage(out damage);
        }

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
                OnClick?.Invoke(this);
            }
        #endregion

        #region ICard
            public virtual void SetInteractable(bool interactable)
            {
                Interactable = interactable;
            }

            public void Use(Action onComplete = null)
            {
                BattleCardData.GetRandomAnimation(out var anima);
                OnUse?.Invoke(this, anima, () => onComplete?.Invoke());
            }

            public void Destroy()
            {
                Destroy(gameObject);
            }

            #region Card Order
                public virtual void SetCardOrder(GameObject cardOrderPrefab)
                {
                    if (CardOrder is not null)
                        Destroy(CardOrder);
                    CardOrder = Instantiate(cardOrderPrefab, CardOrderParent);
                }
                
                public virtual void RemoveCardOrder()
                {
                    if (CardOrder is null) return;
                    Destroy(CardOrder);
                    CardOrder = null;
                }
            #endregion
            
            #region AnimationSystem
            public virtual void Move(Transform parent, DoAnchorPos settings, Action onComplete = null)
            {
                CardRect.SetParent(parent);
                AnimationSystem.MoveTo(CardRect, settings)
                    .OnComplete(() => onComplete?.Invoke());
            }

            public virtual void MoveAndFlip(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null)
            {
                CardRect.SetParent(parent);
                
                flipSettings.GetValues(out var rotateSettings, out var scaleSettings01, out var scaleSettings02);
                
                AnimationSystem.MoveTo(CardRect, anchorPosSettings)
                    .OnComplete(() => AnimationSystem.FlipToFront(CardSurfaceRect, rotateSettings, scaleSettings01, scaleSettings02)
                        .OnComplete(() => onComplete?.Invoke()));
            }
            #endregion
        #endregion
    }
}