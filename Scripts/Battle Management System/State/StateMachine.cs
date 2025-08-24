namespace Battle_Management_System.State
{
    internal class StateMachine
    {
        private IState CurrentState;

        public void ChangeState(IState newState)
        {
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void ExitState()
        {
            CurrentState?.Exit();
        }
    }
}