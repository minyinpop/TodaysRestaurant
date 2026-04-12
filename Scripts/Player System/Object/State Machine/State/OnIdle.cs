using System;

namespace Player_System.Object.State_Machine.State
{
    public sealed class OnIdle : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnIdle(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Enter() => OnEnter.Invoke();
        public void Exit() => OnExit.Invoke();
    }
}