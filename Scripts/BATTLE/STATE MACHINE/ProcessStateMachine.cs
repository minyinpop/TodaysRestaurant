namespace BATTLE.STATE_MACHINE
{
    internal class ProcessStateMachine
    {
        private IProcessState CurrentState;

        public void ChangeState(IProcessState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}