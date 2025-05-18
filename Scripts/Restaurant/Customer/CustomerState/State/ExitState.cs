namespace Restaurant.Customer.CustomerState.State
{
    internal class ExitState : IState
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