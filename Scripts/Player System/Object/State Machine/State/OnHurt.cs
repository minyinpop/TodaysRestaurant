using System;

namespace Player_System.Object.State_Machine.State
{
    public sealed class OnHurt : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnHurt(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Enter() => OnEnter.Invoke();
        public void Exit() => OnExit.Invoke();
    }
}