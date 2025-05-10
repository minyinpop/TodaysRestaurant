namespace Restaurant.Kitchenware.StateMachine.BubbleState
{
    public class BurnBubble : IBubbleState
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
            Manager.SetBubbleInteractable(true);
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractable(false);
        }

        public void OnClick()
        {
            Manager.ChangeState(new EmptyBubble());
        }
    }
}