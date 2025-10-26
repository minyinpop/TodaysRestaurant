namespace System.Economy.Child.Customer.Main.State_Machine.State
{
    internal sealed class ShowOrderItem : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public ShowOrderItem(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}