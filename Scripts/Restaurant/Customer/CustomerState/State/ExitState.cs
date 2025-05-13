namespace Restaurant.Customer.CustomerState.State
{
    public class ExitState : ICustomerState
    {
        private CustomerManager Manager { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            Manager.WalkToEntrance();
            Manager.PlayWalkAnima();
        }

        public void Exit()
        {
            
        }
    }
}