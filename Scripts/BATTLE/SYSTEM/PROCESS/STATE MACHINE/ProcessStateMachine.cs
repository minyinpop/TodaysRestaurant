namespace BATTLE.SYSTEM.PROCESS.STATE_MACHINE
{
    internal class ProcessStateMachine
    {
        private IProcessState CurrentState;

        public void ChangeState(ProcessSystem system, IProcessState newState)
        {
            CurrentState = newState;
            CurrentState?.Enter(system);
        }
    }
}