namespace System.Battle.System.Main.State_Machine.State
{
    internal sealed class OnPlayerWin : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnPlayerWin(Action onEnter, Action onExit)
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