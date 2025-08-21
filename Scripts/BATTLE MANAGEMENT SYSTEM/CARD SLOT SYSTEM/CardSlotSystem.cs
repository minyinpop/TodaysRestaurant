using UnityEngine;

namespace BATTLE_MANAGEMENT_SYSTEM.CARD_SLOT_SYSTEM
{
    internal class CardSlotSystem : MonoBehaviour
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