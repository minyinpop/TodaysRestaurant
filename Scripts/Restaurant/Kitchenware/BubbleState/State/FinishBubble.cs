namespace Restaurant.Kitchenware.BubbleState.State
{
    public class FinishBubble : IBubbleState
    {
        private KitchenwareManager Manager { get; set; }
        
        public void Enter(KitchenwareManager manager)
        {
            Manager = manager;
            Manager.InitFinishBubble();
        }

        public void Exit()
        {
            Manager.DestroyBubble();
            Manager.ClearDish();
        }

        public void PlayerEnter()
        {
            Manager.SetBubbleInteractable(true);
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractable(false);
        }

        public void OnClick()
        {
            Manager.GetDish();
        }
    }
}