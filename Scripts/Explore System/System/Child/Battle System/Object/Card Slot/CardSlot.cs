using System;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Card_Slot
{
    internal sealed class CardSlot : MonoBehaviour
    {
        private Card.Card _card;

        public void Set(Card.Card card)
        {
            if (!IsEmpty())
            {
                Debug.Log($"{nameof(card)} 不能傳入空值。");
                return;
            }
            
            _card = card;
        }

        public bool Get(out Card.Card card)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException($"{name} 的 {_card} 是空的。");
            }

            card = _card;
            _card = null;
            
            return true;
        }
        
        public bool IsEmpty()
        {
            return _card is null;
        }

        public bool Compare(Card.Card card)
        {
            return card == _card;
        }
    }
}