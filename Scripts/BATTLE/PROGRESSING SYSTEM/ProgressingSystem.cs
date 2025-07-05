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
            nextState.Finish += OnBeginningStateFinish;
        }
        
        
        
        /// <summary>
        /// 當 OnBeginning 裡的 Enter() 結束後，就會自動觸發這個 Method
        /// </summary>
        private void OnBeginningStateFinish()
        {
            StateMachine.ChangeState(new OnBattleInitiative());
        }
    }
}