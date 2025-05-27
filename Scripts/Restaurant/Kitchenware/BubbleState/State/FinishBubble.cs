namespace Restaurant.Kitchenware.BubbleState.State
{
    internal class FinishBubble : IBubbleState
    {
        private KitchenwareManager Manager { get; set; }
        
        public void Enter(KitchenwareManager manager)
        {
            Manager = manager;
            Manager.InitFinishBubble();
            Manager.StartConversation();
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
            Manager.GetDish();
        }
    }
}