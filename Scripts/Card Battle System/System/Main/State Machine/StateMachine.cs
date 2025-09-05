namespace Card_Battle_System.System.Main.State_Machine
{
    internal sealed class StateMachine
    {
        private IState CurrentState;

        public void ChangeState(IState NewState)
        {
            CurrentState?.Exit();
            CurrentState = NewState;
            CurrentState.Enter();
        }
    }
}