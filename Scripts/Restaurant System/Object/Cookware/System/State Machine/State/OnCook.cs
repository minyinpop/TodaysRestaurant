using System;

namespace Restaurant_System.Object.Cookware.System.State_Machine.State
{
    internal sealed class OnCook : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnInteract;
        private readonly Action OnExit;
        
        public OnCook(Action onEnter, Action onInteract, Action onExit)
        {
            OnEnter = onEnter;
            OnInteract = onInteract;
            OnExit = onExit;
        }

        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Interact()
        {
            OnInteract?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}