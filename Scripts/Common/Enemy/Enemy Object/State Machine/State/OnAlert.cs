using System;

namespace Common.Enemy.Enemy_Object.State_Machine.State
{
    public sealed class OnAlert : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnAlert(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Enter() => OnEnter?.Invoke();
        public void Exit() => OnExit?.Invoke();
    }
}