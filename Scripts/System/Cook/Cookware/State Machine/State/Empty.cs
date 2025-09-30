namespace System.Cook.Cookware.State_Machine.State
{
    internal sealed class Empty : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public Empty(Action onEnter, Action onExit)
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