namespace Battle_Management_System.Decisive_Coin_System.State_Machine
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