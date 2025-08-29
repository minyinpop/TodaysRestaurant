using System;
using Animation_Settings;
using UnityEngine;

namespace Battle.Card.Base
{
    internal interface ICard
    {
        #region CardSO
            public void GetDrawChance(out float Chance);
        #endregion
        
        #region Animation
            public void MoveToParent(Transform Parent, DOAnchorPosSettings Settings, Action OnComplete = null);
        #endregion
    }
}