using System;

namespace Restaurant_System.Object.Cookware.System.State_Machine.State
{
    internal sealed class OnOvercooked : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;

        public OnOvercooked(Action onEnter, Action onExit)
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