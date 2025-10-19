namespace System.Cook.Main.State_Machine.State
{
    internal sealed class OnCookStart : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnCookStart(Action onEnter, Action onExit)
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