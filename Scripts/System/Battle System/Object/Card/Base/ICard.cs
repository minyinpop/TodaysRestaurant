using Data.Animation.DOTween.Basic;
using Data.Animation.DOTween.Combine;
using UnityEngine;

namespace System.Battle_System.Object.Card.Base
{
    internal interface ICard
    {
        public event Action<ICard> OnClick;
        
        public void SetInteractable(bool interactable);
        public void Use(Action haveEnemyAlive, Action enemyAllDeath);
        public void Destroy();
        
        #region Card Order
            public void SetCardOrder(GameObject cardOrderPrefab);
            public void RemoveCardOrder();
        #endregion
        
        #region AnimationSystem
            public void Move(Transform parent, DoAnchorPos settings, Action onComplete = null);
            public void MoveAndFlip(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null);
        #endregion
    }
}