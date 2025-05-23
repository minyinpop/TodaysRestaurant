namespace Restaurant.Customer.CustomerState.State
{
    internal class WalkToSeat : IState
    {
        private CustomerManager Manager { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            manager.PlayWalkAnima();
            manager.WalkToSeat();
        }
    }
}