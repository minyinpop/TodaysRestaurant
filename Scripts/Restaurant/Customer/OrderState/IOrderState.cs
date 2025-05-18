namespace Restaurant.Customer.OrderState
{
    internal interface IOrderState
    {
        public void Enter();
        
        public void Exit();
    }
}