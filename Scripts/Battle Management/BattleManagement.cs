using System;
using Battle_Management.Card_Pool;
using Battle_Management.State;
using Battle_Management.State.Type;
using UnityEngine;

namespace Battle_Management
{
    internal class BattleManagement : MonoBehaviour
    {
        private readonly StateMachine StateMachine = new();

        [field: Header("System")]
        [field: SerializeField] private CardPool CardPool;

        private void Start()
        {
            OnBattleStart();
        }
        
        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(EnterOnBattleStart, ExitOnBattleStart));
                }

                private void EnterOnBattleStart()
                {
                    RefillCardPool(() =>
                    {
                        Debug.Log("Card Pool Refilled.");
                    });
                }

                private void ExitOnBattleStart()
                {
                }
            #endregion
        #endregion
        
        #region CardPool
            private void RefillCardPool(Action OnComplete = null)
            {
                CardPool.Refill(OnComplete);
            }
        #endregion
    }
}