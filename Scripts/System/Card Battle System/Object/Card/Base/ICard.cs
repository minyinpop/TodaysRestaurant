using Data.DOTween.Basic;
using Data.DOTween.Combine;
using UnityEngine;

namespace System.Card_Battle_System.Object.Card.Base
{
    internal interface ICard
    {
        public void SetInteractable(bool interactable);
        
        public void MoveToParent(Transform parent, DoAnchorPos settings, Action onComplete = null);

        public void MoveToShowPoint(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null);
    }
}