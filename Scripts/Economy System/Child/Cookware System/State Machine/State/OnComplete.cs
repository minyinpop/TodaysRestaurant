using System;

namespace Economy_System.Child.Cookware_System.State_Machine.State
{
    internal sealed class OnComplete : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnComplete(Action onEnter, Action onExit)
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