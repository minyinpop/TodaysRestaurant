namespace Restaurant.Kitchenware.BubbleState.State
{
    public class EmptyBubble : IBubbleState
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
            Manager.SetBubbleInteractable(true);
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractable(false);
            Manager.SetCookMenuVisible(false);
        }
        
        public void OnClick()
        {
            Manager.SetCookMenuVisible(true);
        }
    }
}