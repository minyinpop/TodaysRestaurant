using System;

namespace Restaurant_System.Object.Creature.Customer.System.Main.State_Machine.State
{
    public sealed class Happy : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public Happy(Action onEnter, Action onExit)
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