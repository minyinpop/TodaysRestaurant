using System;

namespace Restaurant_System.Object.Creature.Customer.System.Main.State_Machine.State
{
    public sealed class WalkToEntrance : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public WalkToEntrance(Action onEnter, Action onExit)
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