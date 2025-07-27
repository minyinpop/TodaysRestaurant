namespace BATTLE.SYSTEM.PROCESS.STATE_MACHINE.STATE
{
    internal class FocusOnEnemy : IProcessState
    {
        private ProcessSystem ProcessSystem;
        
        public void Enter(ProcessSystem system)
        {
            ProcessSystem = system;
            ProcessSystem.ChangeState(new WaitForInitiativeResult());
        }
    }
}