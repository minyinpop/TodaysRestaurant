using UnityEngine;

namespace BATTLE.CARD.STATE_MACHINE
{
    internal class CardStateMachine
    {
        private ICardState CurrentState { get; set; }

        public void SetState(ICardState newState, GameObject card)
        {
            CurrentState = newState;
            CurrentState.Enter(card);
        }

        public void ChangeState(ICardState newState, GameObject card)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter(card);
        }
    }
}