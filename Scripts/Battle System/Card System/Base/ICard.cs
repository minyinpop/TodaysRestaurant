using System;
using DoTween_Settings;
using UnityEngine;

namespace Battle_System.Card_System.Base
{
    internal interface ICard
    {
        public void GetDrawChance(out float DrawChance);
        
        public void MoveToParent(Transform Parent, DoAnchorPosSettings Settings, Action OnComplete = null);
        public void FlipCard();
    }
}