using UnityEngine;

namespace Battle.Object.Card_Slot
{
    internal sealed class CardSlot : MonoBehaviour
    {
        private GameObject Card;

        public void Add(GameObject Card)
        {
            this.Card = Card;
        }
        
        public void Get(out GameObject Card)
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