using BATTLE.CARD.STATE_MACHINE;
using UnityEngine;

namespace BATTLE.CARD.STATE
{
    internal class ClickState : ICardState
    {
        private GameObject Card { get; set; }
        
        public void Enter(GameObject card)
        {
            Card = card;
        }

        public void Exit()
        {
            
        }
    }
}