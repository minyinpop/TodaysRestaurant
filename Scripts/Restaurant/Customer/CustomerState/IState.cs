namespace Restaurant.Customer.CustomerState
{
    internal interface IState
    {
        public void Enter(CustomerManager manager);

        public void Exit();
    }
}