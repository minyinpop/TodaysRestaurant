namespace System.Card_Battle_System.System.Main.State_Machine.State
{
    internal sealed class OnPlayerTurn : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnPlayerTurn(Action onEnter, Action onExit)
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