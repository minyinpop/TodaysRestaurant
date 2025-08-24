using System;
using UnityEngine;

namespace Battle_Management.Card.Base
{
    internal interface ICard
    {
        public void GetDrawChance(out float chance);
        public void MoveToParent(Transform parent, float duration = 1f, Action OnComplete = null);
    }
}