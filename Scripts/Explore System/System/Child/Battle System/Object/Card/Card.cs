using System;
using Animation_System.DOTween.Basic;
using Animation_System.DOTween.Combine;
using Common.Pointer_Event;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Card
{
    public abstract class Card : PointerEvent
    {
        [field: Header("卡片資料")]
        [field: SerializeField] protected CardSO cardData;
                                public CardSO CardData => cardData;
        
        [field: HideInInspector] public bool Interactable;
        
        public event Action<Card> OnHover;
        public event Action<Card> OnHoverExit;
        public event Action<Card> OnClick;
        
        protected void OnHoverEvent()
        {
            if (OnHover is null)
            {
                Debug.Log($"{nameof(OnHover)} 沒有其它 class 訂閱。");
                return;
            }
            
            OnHover.Invoke(this);
        }
        
        protected void OnHoverExitEvent()
        {
            if (OnHoverExit is null)
            {
                Debug.Log($"{nameof(OnHoverExit)} 沒有其它 class 訂閱。");
                return;
            }
            
            OnHoverExit.Invoke(this);
        }

        protected void OnClickEvent()
        {
            if (OnClick is null)
            {
                Debug.Log($"{nameof(OnClick)} 沒有其它 class 訂閱。");
                return;
            }
            
            OnClick.Invoke(this);
        }
        
        #region Main Function
            public virtual void Use(Action haveEnemyAlive, Action enemyAllDeath) =>
                throw new NotImplementedException();
            public virtual void DestroyCard(Action onComplete) =>
                throw new NotImplementedException();
        #endregion
        
        #region Order
            public virtual void SetCardOrder(GameObject cardOrderPrefab) =>
                throw new NotImplementedException();
            public virtual void RemoveCardOrder() =>
                throw new NotImplementedException();
        #endregion
        
        #region Animation
            public virtual void Move(Transform parent, DoAnchorPos settings, Action onComplete = null) =>
                throw new NotImplementedException();
            public virtual void MoveAndFlip(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null) =>
                throw new NotImplementedException();
        #endregion
    }
}