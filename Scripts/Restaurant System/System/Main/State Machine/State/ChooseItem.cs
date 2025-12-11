using System;

namespace Restaurant_System.System.Main.State_Machine.State
{
    internal sealed class ChooseItem : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public ChooseItem(Action onEnter, Action onExit)
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