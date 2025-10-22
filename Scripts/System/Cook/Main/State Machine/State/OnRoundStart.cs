namespace System.Cook.Main.State_Machine.State
{
    internal class OnRoundStart : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnRoundStart(Action onEnter, Action onExit)
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