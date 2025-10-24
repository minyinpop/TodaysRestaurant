namespace System.Cook.Child.Customer.Object.Main.State_Machine.State
{
    internal sealed class WalkToSeat : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public WalkToSeat(Action onEnter, Action onExit)
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