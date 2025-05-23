namespace Restaurant.Customer.BubbleState
{
    internal interface IBubbleState
    {
        public void Enter(CustomerManager manager);

        public void Exit();

        public void PlayerEnter();
        
        public void PlayerLeave();

        public void OnClick();
    }
}