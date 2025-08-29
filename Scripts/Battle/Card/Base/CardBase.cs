using System;
using Animation_Settings;
using Battle.Card.Animation;
using Battle.Card.Data;
using UnityEngine;

namespace Battle.Card.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class CardBase : MonoBehaviour, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private CardSO CardData;
        
        #region ICard
            public void GetDrawChance(out float Chance)
            {
                CardData.GetDrawChance(out Chance);
            }

            public void MoveToParent(Transform Parent, DOAnchorPosSettings Settings, Action OnComplete = null)
            {
                Rect.SetParent(Parent);
                MoveTo(Settings, OnComplete);
            }
        #endregion
        
        #region AnimationSystem
            private void MoveTo(DOAnchorPosSettings Settings, Action OnComplete = null)
            {
                AnimationSystem.MoveTo(Settings, OnComplete);
            }
        #endregion
    }
}