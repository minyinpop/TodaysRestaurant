namespace Restaurant.Customer.OrderState
{
    internal class OrderStateMachine
    {
        private IOrderState CurrentState { get; set; }

        public void SetState(IOrderState newState)
        {
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void ChangeState(IOrderState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }
}