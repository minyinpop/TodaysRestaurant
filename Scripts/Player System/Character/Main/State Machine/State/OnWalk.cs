using System;

namespace Player_System.Character.Main.State_Machine.State
{
    internal sealed class OnWalk : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnWalk(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Enter() => OnEnter?.Invoke();
        public void Exit() => OnExit?.Invoke();
    }
}