using System;
using Common.Pointer_Event;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Card
{
    public abstract class Card : PointerEvent
    {
        [field: Header("父資料 - 卡片資料")]
        [field: SerializeField] protected CardSO cardData;
                                public CardSO CardData => cardData;
        
        [field: HideInInspector] public bool Interactable;
        
        public event Action<Card> OnHover;
        public event Action<Card> OnHoverExit;
        public event Action<Card> OnClick;
        
        protected void InvokeOnHover()
        {
            if (OnHover is null)
            {
                Debug.Log($"{nameof(OnHover)} 沒有其它 class 訂閱。");
                return;
            }
            
            OnHover.Invoke(this);
        }
        
        protected void InvokeOnHoverExit()
        {
            if (OnHoverExit is null)
            {
                Debug.Log($"{nameof(OnHoverExit)} 沒有其它 class 訂閱。");
                return;
            }
            
            OnHoverExit.Invoke(this);
        }

        protected void InvokeOnClick()
        {
            if (OnClick is null)
            {
                Debug.Log($"{nameof(OnClick)} 沒有其它 class 訂閱。");
                return;
            }
            
            OnClick.Invoke(this);
        }

        public abstract void Use(Action haveEnemyAlive, Action enemyAllDeath);
        
        public abstract void DestroyCard(Action onComplete);
    }
}