using System;
using Animation_System.DOTween.Basic;
using Animation_System.DOTween.Combine;
using Common;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Battle_System.Object.Card
{
    public abstract class Card : PointerEvent, ICard
    {
        public event Action<ICard> OnClick;
        protected void OnClickEvent() => OnClick?.Invoke(this);
        
        #region Information
            public virtual void GetCardType(out CardType type) =>
                    throw new NotImplementedException();
            public virtual void GetDrawChance(out float chance) =>
                throw new NotImplementedException();
            public virtual void GetDamage(out Damage damage) =>
                throw new NotImplementedException();
        #endregion
            
        #region Status
            public virtual void SetInteractable(bool interactable) =>
                throw new NotImplementedException();
        #endregion
        
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