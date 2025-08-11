using BATTLE.PROCESS.STATE_MACHINE;

namespace BATTLE.PROCESS.STATE_TYPE
{
    internal class SpawnInitiativeCoinAndReadyToToss : IBattleProcessState
    {
        private BattleProcessSystem BattleProcessSystem;
        
        public void OnEnter(BattleProcessSystem system)
        {
            BattleProcessSystem = system;
        }

        public void OnExit()
        {
            
        }
    }
}