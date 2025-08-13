namespace DECISIVE_COIN_SYSTEM.STATE_MACHINE
{
    internal class StateMachine
    {
        private IState CurrentState;

        public void ChangeState(DecisiveCoinSystem system, IState newState)
        {
            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState?.OnEnter(system);
        }
    }
}