using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.CARD_POOL_SLOT
{
    internal class CardPoolSlot : MonoBehaviour
    {
        private GameObject Card;

        public void AddCard(GameObject card)
        {
            Card = card;
        }

        public void RemoveCard()
        {
        }

        public bool IsEmpty()
        {
            return Card is null;
        }
    }
}