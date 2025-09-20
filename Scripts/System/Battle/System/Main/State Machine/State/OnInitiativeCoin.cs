namespace System.Battle.System.Main.State_Machine.State
{
    internal sealed class OnInitiativeCoin : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnInitiativeCoin(Action onEnter, Action onExit)
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