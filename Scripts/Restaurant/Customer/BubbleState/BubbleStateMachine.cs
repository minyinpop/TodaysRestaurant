namespace Restaurant.Customer.BubbleState
{
    public class BubbleStateMachine
    {
        private IBubbleState CurrentBubbleState { get; set; }

        public void SetState(IBubbleState newState, CustomerManager manager)
        {
            CurrentBubbleState = newState;
            CurrentBubbleState.Enter(manager);
        }

        public void ChangeState(IBubbleState nextState, CustomerManager manager)
        {
            CurrentBubbleState.Exit();
            CurrentBubbleState = nextState;
            CurrentBubbleState.Enter(manager);
        }

        public void PlayerEnter()
        {
            CurrentBubbleState.PlayerEnter();
        }
        
        public void PlayerLeave()
        {
            CurrentBubbleState.PlayerLeave();
        }
        
        public void OnClick()
        {
            CurrentBubbleState.OnClick();
        }
    }
}