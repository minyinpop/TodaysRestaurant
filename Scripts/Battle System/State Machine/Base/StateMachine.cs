namespace Battle_System.State_Machine.Base
{
    internal sealed class StateMachine
    {
        private IState CurrentState;
        
        public void ChangeState(IState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}