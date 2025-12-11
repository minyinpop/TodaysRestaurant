using System;

namespace Economy_System.Child.Creature.Customer.System.Main.State_Machine.State
{
    internal sealed class WatchFoodMenu : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public WatchFoodMenu(Action onEnter, Action onExit)
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