using System;
using Animation_System.DOTween.Basic;
using Animation_System.DOTween.Combine;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Battle_System.Object.Card
{
    public interface ICard
    {
        public event Action<ICard> OnClick;
        
        #region Information
            public void GetCardType(out CardType type);
            public void GetDrawChance(out float chance);
            public void GetDamage(out Damage damage);
        #endregion
        
        #region Status
            public void SetInteractable(bool interactable);
        #endregion
        
        #region Main Function
            public void Use(Action haveEnemyAlive, Action enemyAllDeath);
            public void DestroyCard(Action onComplete);
        #endregion
        
        #region Order
            public void SetCardOrder(GameObject cardOrderPrefab);
            public void RemoveCardOrder();
        #endregion
        
        #region Animation
            public void Move(Transform parent, DoAnchorPos settings, Action onComplete = null);
            public void MoveAndFlip(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null);
        #endregion
    }
}