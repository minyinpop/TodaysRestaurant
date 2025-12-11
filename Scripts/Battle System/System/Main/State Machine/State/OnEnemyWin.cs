using System;

namespace Battle_System.System.Main.State_Machine.State
{
    internal sealed class OnEnemyWin : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnEnemyWin(Action onEnter, Action onExit)
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