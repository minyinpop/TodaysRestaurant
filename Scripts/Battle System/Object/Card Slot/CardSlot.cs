using Battle_System.Object.Card.Base;
using UnityEngine;

namespace Battle_System.Object.Card_Slot
{
    internal sealed class CardSlot : MonoBehaviour
    {
        private GameObject Card;

        public bool Add(GameObject card)
        {
            if (!card.TryGetComponent<ICard>(out _)) return false;
            Card = card;
            return true;
        }

        public bool Get(out GameObject card)
        {
            if (Card is null)
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
    }
}