namespace Restaurant.Customer.CustomerState
{
    public interface ICustomerState
    {
        public void Enter(CustomerManager manager);

        public void Exit();
    }
}