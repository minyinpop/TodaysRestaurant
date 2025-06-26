using BATTLE.CARD.BATTLE;

namespace BATTLE.CARD.STATE
{
    internal class CardStateMachine
    {
        private ICardState CurrentState { get; set; }
        
        public void ChangeState(ICardState newState, BattleCardBase card)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter(card);
        }
    }
}