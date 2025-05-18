namespace Restaurant.Customer.CustomerState.State
{
    internal class EnterState : IState
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