namespace Restaurant.Customer.CustomerState
{
    internal class StateMachine
    {
        private IState CurrentState { get; set; }

        public void SetState(IState newState, CustomerManager manager)
        {
            CurrentState = newState;
            CurrentState.Enter(manager);
        }

        public void ChangeState(IState nextState, CustomerManager manager)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter(manager);
        }
    }
}