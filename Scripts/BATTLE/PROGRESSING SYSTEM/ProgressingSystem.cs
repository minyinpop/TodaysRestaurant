using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.PROGRESSING_SYSTEM.STATE;
using UnityEngine;

namespace BATTLE.PROGRESSING_SYSTEM
{
    internal class ProgressingSystem : MonoBehaviour
    {
        private StateMachine StateMachine = new();

        private void Start()
        {
            var nextState = new OnBeginning();
            StateMachine.ChangeState(nextState);
            nextState.OnFinish += OnBeginningStateOnFinish;
        }
        
        
        
        /// <summary>
        /// 當 OnBeginning 裡的 Enter() 結束後，就會自動觸發這個 Method
        /// </summary>
        private void OnBeginningStateOnFinish()
        {
            var nextState = new OnBattleInitiative();
            StateMachine.ChangeState(new OnBattleInitiative());
            nextState.OnFinish += OnBattleInitiativeStateOnFinish;
        }

        /// <summary>
        /// 等待玩家投擲完硬幣後，決定玩家或是敵人先手
        /// </summary>
        private void OnBattleInitiativeStateOnFinish()
        {
            // TODO 因為開發方便，暫時先設定為玩家先手，之後依照硬幣投擲的結果來決定
            StateMachine.ChangeState(new OnPlayerRound());
        }
    }
}