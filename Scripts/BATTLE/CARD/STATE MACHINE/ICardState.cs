using UnityEngine;

namespace BATTLE.CARD.STATE_MACHINE
{
    internal interface ICardState
    {
        public void Enter(GameObject card);
        public void Exit();
    }
}