namespace Restaurant.Customer.BubbleState
{
    public interface IBubbleState
    {
        public void Enter(CustomerManager manager);

        public void Exit();

        public void PlayerEnter();
        
        public void PlayerLeave();

        public void OnClick();
    }
}