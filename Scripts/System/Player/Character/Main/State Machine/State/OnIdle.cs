namespace System.Player.Character.Main.State_Machine.State
{
    internal sealed class OnIdle : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnIdle(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Enter() => OnEnter?.Invoke();
        public void Exit() => OnExit?.Invoke();
    }
}