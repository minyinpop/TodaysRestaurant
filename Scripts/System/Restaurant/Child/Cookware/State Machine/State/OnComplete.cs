namespace System.Restaurant.Child.Cookware.State_Machine.State
{
    internal sealed class OnComplete : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnComplete(Action onEnter, Action onExit)
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