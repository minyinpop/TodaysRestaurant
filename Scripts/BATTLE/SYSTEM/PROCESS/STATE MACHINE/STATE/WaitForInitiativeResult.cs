using BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE.STATE;

namespace BATTLE.SYSTEM.PROCESS.STATE_MACHINE.STATE
{
    internal class WaitForInitiativeResult : IProcessState
    {
        private ProcessSystem ProcessSystem;
        
        public void Enter(ProcessSystem system)
        {
            ProcessSystem = system;
            
            ProcessSystem.ShowScreenMask(() =>
            {
                ProcessSystem.ChangeState(new ReadyToTossCoin());
                ProcessSystem.HideScreenMask();
            });
        }
    }
}