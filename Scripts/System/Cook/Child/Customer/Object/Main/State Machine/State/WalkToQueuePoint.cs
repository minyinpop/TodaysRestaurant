namespace System.Cook.Child.Customer.Object.Main.State_Machine.State
{
    internal sealed class WalkToQueuePoint : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public WalkToQueuePoint(Action onEnter, Action onExit)
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