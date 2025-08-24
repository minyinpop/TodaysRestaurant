using Battle_Management_System.Card_Pool_System;
using Battle_Management_System.State;
using Battle_Management_System.State.Type;
using UnityEngine;

namespace Battle_Management_System
{
    internal class BattleManagementSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        
        private readonly StateMachine StateMachine = new StateMachine();

        private void Start()
        {
            OnBattleStart();
        }
        
        #region StateMachine
            private void ChangeState(IState newState)
            {
                StateMachine.ChangeState(newState);
            }

            private void ExitState()
            {
                StateMachine.ExitState();
            }
            
            #region OnBattleStart
                private void OnBattleStart()
                {
                    ChangeState(new OnBattleStart(EnterOnBattleStartState, ExitOnBattleStartState));
                }
                
                private void EnterOnBattleStartState()
                {
                }
                
                private void ExitOnBattleStartState()
                {
                }
            #endregion
        #endregion
    }
}