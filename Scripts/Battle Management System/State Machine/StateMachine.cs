namespace Battle_Management_System.State_Machine
{
    internal class StateMachine
    {
        private IState CurrentState;

        public void ChangeState(IState newState)
        {
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}