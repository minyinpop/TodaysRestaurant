using System;

namespace Explore_System.Main.State_Machine.State
{
    public class InitializeLevel
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public InitializeLevel(Action onEnter, Action onExit)
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