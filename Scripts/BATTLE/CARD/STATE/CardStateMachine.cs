using UnityEngine;

namespace BATTLE.CARD.STATE
{
    internal class CardStateMachine
    {
        private ICardState CurrentState { get; set; }
        
        public void ChangeState(ICardState newState, GameObject card)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter(card);
        }

        public void OnPointerEnter()
        {
            CurrentState.OnPointerEnter();
        }

        public void OnPointerExit()
        {
            CurrentState.OnPointerExit();
        }
        
        public void OnPointerClick()
        {
            CurrentState.OnPointerClick();
        }
    }
}