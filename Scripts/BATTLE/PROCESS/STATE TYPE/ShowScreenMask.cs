using BATTLE.PROCESS.STATE_MACHINE;
using UnityEngine;

namespace BATTLE.PROCESS.STATE_TYPE
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
            Debug.Log("OnExit Method Triggerred.");
        }
    }
}