using BATTLE.PROCESS_SYSTEM.STATE_MACHINE;

namespace BATTLE.PROCESS_SYSTEM.STATE_TYPE
{
    internal class SpawnInitiativeCoinAndReadyToToss : IBattleProcessState
    {
        private BattleProcessSystem BattleProcessSystem;
        
        public void OnEnter(BattleProcessSystem system)
        {
            BattleProcessSystem = system;
            BattleProcessSystem.SpawnInitiativeBattleCoin();
        }

        public void OnExit()
        {
            
        }
    }
}