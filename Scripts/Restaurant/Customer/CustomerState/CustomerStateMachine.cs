namespace Restaurant.Customer.CustomerState
{
    public class CustomerStateMachine
    {
        private ICustomerState CurrentState { get; set; }

        public void SetState(ICustomerState newState, CustomerManager manager)
        {
            CurrentState = newState;
            CurrentState.Enter(manager);
        }

        public void ChangeState(ICustomerState nextState, CustomerManager manager)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter(manager);
        }
    }
}