using System;
using UnityEngine;

namespace Battle_Management_System.Card_System.Card_System
{
    internal interface ICard
    {
        public int GetDrawChance();
        public void MoveCardToSlot(Transform parent, Action onComplete = null);
        public void MoveCardToSlotAndFlip(Transform parent, Action onComplete);
    }
}