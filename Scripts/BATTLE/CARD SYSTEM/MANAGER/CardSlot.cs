using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER
{
    internal class CardSlot : MonoBehaviour
    {
        private GameObject Card;

        public void AddCard(GameObject card)
        {
            Card = card;
        }
        
        public void GetCard(out GameObject card)
        {
            card = Card;
            Card = null;
        }

        public bool IsEmpty()
        {
            return Card is null;
        }
    }
}