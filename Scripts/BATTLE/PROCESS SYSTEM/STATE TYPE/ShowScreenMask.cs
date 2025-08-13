using BATTLE.PROCESS_SYSTEM.STATE_MACHINE;

namespace BATTLE.PROCESS_SYSTEM.STATE_TYPE
{
    internal class ShowScreenMask : IBattleProcessState
    {
        private BattleProcessSystem BattleProcessSystem;
        
        public void OnEnter(BattleProcessSystem system)
        {
            BattleProcessSystem = system;
            BattleProcessSystem.ShowScreenMask(OnExit);
        }

        public void OnExit()
        {
            BattleProcessSystem.ChangeStateToSpawnInitiativeCoinAndReadyToToss();
        }
    }
}