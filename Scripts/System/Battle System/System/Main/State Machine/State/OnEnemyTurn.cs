namespace System.Battle_System.System.Main.State_Machine.State
{
    internal sealed class OnEnemyTurn : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnEnemyTurn(Action onEnter, Action onExit)
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