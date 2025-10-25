namespace System.Economy.Child.Cookware.State_Machine.State
{
    internal sealed class OnCook : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnCook(Action onEnter, Action onExit)
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