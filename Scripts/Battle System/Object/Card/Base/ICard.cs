using System;
using Data.DOTween_Values;
using UnityEngine;

namespace Battle_System.Object.Card.Base
{
    internal interface ICard
    {
        public void GetDrawChance(out float GetDrawChance);

        public void MoveToParent(Transform Parent, AnchorPosValue AnchorPosValue, Action OnComplete = null);
    }
}