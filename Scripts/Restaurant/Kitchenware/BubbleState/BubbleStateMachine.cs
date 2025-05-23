namespace Restaurant.Kitchenware.BubbleState
{
    internal class BubbleStateMachine
    {
        private IBubbleState CurrentBubbleState { get; set; }

        public void SetState(IBubbleState newState, KitchenwareManager manager)
        {
            CurrentBubbleState = newState;
            CurrentBubbleState.Enter(manager);
        }

        public void ChangeState(IBubbleState nextState, KitchenwareManager manager)
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