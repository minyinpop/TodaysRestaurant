using System;

namespace Restaurant_System.System.Main.State_Machine.State
{
    public class RestaurantState : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public RestaurantState(Action onEnter, Action onExit)
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