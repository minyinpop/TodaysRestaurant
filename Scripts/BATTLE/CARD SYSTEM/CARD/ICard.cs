using System;
using BATTLE.CARD_SYSTEM.MANAGER;

namespace BATTLE.CARD_SYSTEM.CARD
{
    internal interface ICard
    {
        public event Action OnShowCardComplete;
        
        public void OnSpawnInCardPool(CardSlot cardSlot);
        public void OnDrawCardAndShow(CardSlot cardSlot);
    }
}