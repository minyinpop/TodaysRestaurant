using System;

namespace Economy_System.Main.State_Machine.State
{
    internal sealed class PlayerChooseFoodOnCookMenu : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public PlayerChooseFoodOnCookMenu(Action onEnter, Action onExit)
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