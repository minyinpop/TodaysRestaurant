namespace Restaurant.Kitchenware.BubbleState.State
{
    internal class BurnBubble : IBubbleState
    {
        private KitchenwareManager Manager { get; set; }
        
        public void Enter(KitchenwareManager manager)
        {
            Manager = manager;
            Manager.InitBurnBubble();
        }

        public void Exit()
        {
            Manager.DestroyBubble();
            Manager.ClearDish();
        }

        public void PlayerEnter()
        {
            Manager.SetBubbleInteractableTrue();
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractableFalse();
        }

        public void OnClick()
        {
            Manager.ChangeState(new EmptyBubble());
            Manager.StartConversation();
            Manager.CloseCoachMask();
            Manager.IsTutorialCanPlayGame = true;
        }
    }
}