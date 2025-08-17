using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.DATA;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM
{
    internal class CardPoolSystem : MonoBehaviour
    {
        [field: SerializeField] private PlayerDeckSO PlayerDeckSO;

        public void DrawCard(int drawNumber)
        {
            for (var i = 0; i < drawNumber; i++)
            {
                var randomCard = PickupRandomCardInDeck();
            }
        }
        
        #region Player Deck
            private GameObject PickupRandomCardInDeck() => PlayerDeckSO.GetRandomCard();
        #endregion
    }
}