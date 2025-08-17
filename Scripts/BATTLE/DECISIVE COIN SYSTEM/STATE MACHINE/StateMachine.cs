namespace BATTLE.DECISIVE_COIN_SYSTEM.STATE_MACHINE
{
    internal class StateMachine
    {
        private IState CurrentState;

        public void ChangeState(DecisiveCoinSystem system, IState newState)
        {
            CurrentState = newState;
            CurrentState?.OnEnter(system);
        }
    }
}