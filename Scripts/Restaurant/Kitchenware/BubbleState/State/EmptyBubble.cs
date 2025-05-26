namespace Restaurant.Kitchenware.BubbleState.State
{
    internal class EmptyBubble : IBubbleState
    {
        private KitchenwareManager Manager { get; set; }
        
        public void Enter(KitchenwareManager manager)
        {
            Manager = manager;
            Manager.InitEmptyBubble();
        }
        
        public void Exit()
        {
            Manager.DestroyBubble();
        }

        public void PlayerEnter()
        {
            Manager.SetBubbleInteractableTrue();
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractableFalse();
            Manager.CloseCookMenu();
        }
        
        public void OnClick()
        {
            Manager.OpenCookMenu();
            if (Manager.IsTutorialCanPlayGame)
                return;
            Manager.StartConversation();
            Manager.CloseCoachMask();
        }
    }
}