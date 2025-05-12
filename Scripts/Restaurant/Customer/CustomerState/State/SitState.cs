using Restaurant.Customer.BubbleState.State;

namespace Restaurant.Customer.CustomerState.State
{
    public class SitState : ICustomerState
    {
        private CustomerManager Manager { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            manager.SetBubbleState(new ThinkBubble());
            manager.PlaySitAnima();
            manager.OnSeat();
        }

        public void Exit()
        {
            
        }
    }
}