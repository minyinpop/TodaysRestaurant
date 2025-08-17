namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.STATE_MACHINE
{
    internal class StateMachine
    {
        private IState CurrentState;
        
        public void ChangeState(BattleCardBase card, IState newState)
        {
            CurrentState = newState;
            CurrentState?.OnEnter(card);
        }
    }
}