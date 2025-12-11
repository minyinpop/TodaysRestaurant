namespace Battle_System.System.Main.State_Machine
{
    internal sealed class StateMachine
    {
        private IState CurrentState;
        
        public void ChangeState(IState nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState?.Enter();
        }
    }
}