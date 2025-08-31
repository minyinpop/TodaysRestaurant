using System;
using Data.DOTween_Value;
using UnityEngine;

namespace Battle.Object.Card.Base
{
    internal interface ICard
    {
        public void GetDrawChance(out float DrawChance);

        public void MoveToParent(Transform Parent, DoAnchorPosValue DoAnchorPosValue, Action OnComplete = null);

        public void FlipCard(DoFlipValue DoFlipValue, Action OnComplete = null);
    }
}