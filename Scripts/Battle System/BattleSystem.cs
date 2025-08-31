using Battle_System.Child_System;
using Battle_System.State_Machine.Base;
using Battle_System.State_Machine.Type;
using UnityEngine;

namespace Battle_System
{
    internal sealed class BattleSystem : MonoBehaviour
    {
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private DrawCardSystem DrawCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;
        
        private readonly StateMachine StateMachine = new StateMachine();

        private void Start()
        {
            OnBattleStart();
        }
        
        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(OnBattleStartEnter, OnBattleStartExit));
                }

                private void OnBattleStartEnter()
                {
                    CardPoolSystem.Refill(() => Debug.Log("Refill Complete."));
                }

                private void OnBattleStartExit()
                {
                }
            #endregion
        #endregion
    }
}