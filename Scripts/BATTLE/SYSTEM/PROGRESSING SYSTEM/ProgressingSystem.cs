using BATTLE.SYSTEM.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.SYSTEM.PROGRESSING_SYSTEM.STATE_MACHINE.STATE;
using UnityEngine;

namespace BATTLE.SYSTEM.PROGRESSING_SYSTEM
{
    internal class ProgressingSystem : MonoBehaviour
    {
        private BattleStateMachine StateMachine = new();

        private void Start()
        {
            // TODO 設定玩家先開始
            StateMachine.ChangeState(new ChoosingWhoActsFirst());
        }
    }
}