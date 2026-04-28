using System;

namespace Restaurant_System.System.Main.State_Machine.State
{
    internal sealed class OnFoodMenu : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnFoodMenu(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Enter()
        {
            OnEnter.Invoke();
        }

        public void Exit()
        {
            OnExit.Invoke();
        }
    }
}