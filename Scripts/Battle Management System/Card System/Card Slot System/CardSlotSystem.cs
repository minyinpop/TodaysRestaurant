using UnityEngine;

namespace Battle_Management_System.Card_System.Card_Slot_System
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