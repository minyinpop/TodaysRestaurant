namespace System.Cook.Child.Mob.Main.Base.State_Machine.State
{
    internal sealed class OnSearchingForSeat : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnSearchingForSeat(Action onEnter, Action onExit)
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