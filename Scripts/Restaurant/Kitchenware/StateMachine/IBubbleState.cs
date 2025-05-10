namespace Restaurant.Kitchenware.StateMachine
{
    public interface IBubbleState
    {
        public void Enter(KitchenwareManager manager);
        
        public void Exit();

        public void PlayerEnter();
        
        public void PlayerLeave();

        public void OnClick();
    }
}