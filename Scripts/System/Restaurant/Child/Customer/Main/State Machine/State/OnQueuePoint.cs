namespace System.Restaurant.Child.Customer.Main.State_Machine.State
{
    internal sealed class OnQueuePoint : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnQueuePoint(Action onEnter, Action onExit)
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