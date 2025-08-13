using BATTLE.PROCESS_SYSTEM.STATE_MACHINE;
using UnityEngine;

namespace BATTLE.PROCESS_SYSTEM.STATE_TYPE
{
    internal class ShowScreenMask : IBattleProcessState
    {
        private BattleProcessSystem BattleProcessSystem;
        
        public void OnEnter(BattleProcessSystem system)
        {
            Debug.Log("Enter ShowScreenMask State.");
            
            BattleProcessSystem = system;
            BattleProcessSystem.ShowScreenMask(BattleProcessSystem.ChangeStateToSpawnInitiativeCoinAndReadyToToss);
        }

        public void OnExit()
        {
            Debug.Log("Exit ShowScreenMask State.");
        }
    }
}