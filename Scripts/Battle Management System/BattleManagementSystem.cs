using System;
using Battle_Management_System.Card_System;
using Battle_Management_System.Decisive_Coin_System;
using Battle_Management_System.State_Machine;
using Battle_Management_System.State_Type;
using UnityEngine;

namespace Battle_Management_System
{
    internal class BattleManagementSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardSystem CardSystem;
        [field: SerializeField] private DecisiveCoinSystem DecisiveCoinSystem;

        private readonly StateMachine StateMachine = new();

        private void Start()
        {
            OnBattleState();
        }
        
        #region State Machine
            private void ChangeState(IState newState)
            {
                StateMachine.ChangeState(newState);
            }
            
            private void ExitState()
            {
                StateMachine.Exit();
            }
            
            #region On Battle Start
                private void OnBattleState()
                {
                    ChangeState(new OnBattleStart(EnterOnBattleStart, ExitOnBattleStart));
                }

                private void EnterOnBattleStart()
                {
                    Debug.Log("Enter On Battle Start");
                    RefillCardAndDrawOnBattleStart(ExitState);
                }
                
                private void ExitOnBattleStart()
                {
                    Debug.Log("Exit On Battle Start");
                    DecisiveCoinSystem.gameObject.SetActive(true);
                }
            #endregion
        #endregion
        
        #region Card System
            private void RefillCardAndDrawOnBattleStart(Action onComplete = null)
            {
                CardSystem.RefillCardAndDrawOnBattleStart(onComplete);
            }
        #endregion
    }
}