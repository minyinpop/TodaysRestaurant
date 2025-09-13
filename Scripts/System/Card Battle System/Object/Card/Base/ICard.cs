using Data.DOTween.Basic;
using Data.DOTween.Combine;
using UnityEngine;

namespace System.Card_Battle_System.Object.Card.Base
{
    internal interface ICard
    {
        public event Action<ICard> OnClick;
        
        public void SetInteractable(bool interactable);
        
        #region Card Order
            public void SetCardOrder(GameObject cardOrderPrefab);
            public void RemoveCardOrder();
        #endregion
        
        public void Move(Transform parent, DoAnchorPos settings, Action onComplete = null);
        public void MoveAndFlip(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null);
    }
}