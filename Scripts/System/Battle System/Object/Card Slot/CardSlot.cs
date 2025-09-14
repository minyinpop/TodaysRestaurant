using System.Battle_System.Object.Card.Base;
using UnityEngine;

namespace System.Battle_System.Object.Card_Slot
{
    internal sealed class CardSlot : MonoBehaviour
    {
        private ICard Card;

        public bool Set(ICard card)
        {
            if (!IsEmpty()) return false;
            
            Card = card;
            return true;
        }

        public bool Get(out ICard card)
        {
            if (IsEmpty())
            {
                card = null;
                return false;
            }

            card = Card;
            Card = null;
            return true;
        }
        
        public bool IsEmpty()
        {
            return Card is null;
        }
        
        public void SetInteractable(bool interactable)
        {
            Card?.SetInteractable(interactable);
        }

        public bool Compare(ICard card)
        {
            return card == Card;
        }
    }
}