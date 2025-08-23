namespace Battle_Management_System.Card_System.State_Machine
{
    internal class StateMachine
    {
        private IState CurrentState;

        public void ChangeState(IState newState)
        {
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void Exit()
        {
            CurrentState?.Exit();
        }
    }
}