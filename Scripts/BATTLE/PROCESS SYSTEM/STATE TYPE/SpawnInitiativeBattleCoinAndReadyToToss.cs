using BATTLE.PROCESS_SYSTEM.STATE_MACHINE;
using UnityEngine;

namespace BATTLE.PROCESS_SYSTEM.STATE_TYPE
{
    internal class SpawnInitiativeBattleCoinAndReadyToToss : IBattleProcessState
    {
        private BattleProcessSystem BattleProcessSystem;
        
        public void OnEnter(BattleProcessSystem system)
        {
            Debug.Log("Enter Spawn Initiative Coin And Ready To Toss State.");
            
            BattleProcessSystem = system;
            BattleProcessSystem.SpawnInitiativeBattleCoin();
            BattleProcessSystem.MoveInitiativeBattleCoinToTossPoint();
        }

        public void OnExit()
        {
            Debug.Log("Exit Spawn Initiative Coin And Ready To Toss State.");
        }
    }
}