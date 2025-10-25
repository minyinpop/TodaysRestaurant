namespace System.Restaurant.Main.State_Machine.State
{
    internal class RoundStart : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public RoundStart(Action onEnter, Action onExit)
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