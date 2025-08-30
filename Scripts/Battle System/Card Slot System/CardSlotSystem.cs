using UnityEngine;

namespace Battle_System.Card_Slot_System
{
    internal sealed class CardSlotSystem : MonoBehaviour
    {
        private GameObject Card;

        public void AddCard(GameObject Card)
        {
            this.Card = Card;
        }

        public void GetCard(out GameObject Card)
        {
            Card = this.Card;
            this.Card = null;
        }

        public bool IsEmpty()
        {
            return Card is null;
        }
    }
}