using System;

namespace Restaurant_System.System.Main.State_Machine.State
{
    internal class OnRestaurantClosed : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnRestaurantClosed(Action onEnter, Action onExit)
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