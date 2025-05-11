namespace Restaurant.Customer.CustomerState.State
{
    public class EnterState : ICustomerState
    {
        private CustomerManager Manager { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            manager.PlayWalkAnima();
            manager.WalkToSeat();
        }

        public void Exit()
        {
            
        }
    }
}