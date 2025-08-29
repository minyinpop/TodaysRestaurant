using System;
using Battle.Card_Pool;
using Battle.State_Machine;
using Battle.State_Machine.Type;
using UnityEngine;

namespace Battle
{
    internal sealed class BattleManagementSystem : MonoBehaviour
    {
        #region Unity Events
            private void Start()
            {
                OnBattleStart();
            }
        #endregion
        
        #region StateMachine
            private readonly StateMachine StateMachine = new();
        
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(OnBattleStartEnter, OnBattleStartExit));
                }

                private void OnBattleStartEnter()
                {
                    RefillCardPool();
                }

                private void OnBattleStartExit()
                {
                }
            #endregion
        #endregion
        
        #region CardPoolSystem
            [field: Header("Card Pool")]
            [field: SerializeField] private CardPoolSystem CardPoolSystem;

            private void RefillCardPool(Action OnComplete = null)
            {
                CardPoolSystem.Refill(OnComplete);
            }
        #endregion
    }
}