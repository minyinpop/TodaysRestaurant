using System;
using Battle_System.Card_Pool_System;
using Battle_System.Show_Card_System;
using Battle_System.State_Machine;
using Battle_System.State_Machine.Type;
using UnityEngine;

namespace Battle_System
{
    internal sealed class BattleSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;
        
        private readonly StateMachine StateMachine = new StateMachine();

        private void Start()
        {
            OnBattleStart();
        }
        
        #region CardPoolSystem
            private void RefillCardPool(Action OnComplete = null)
            {
                CardPoolSystem.Refill(OnComplete);
            }
        #endregion

        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(OnBattleStartEnter, OnBattleStartExit));
                }

                private void OnBattleStartEnter()
                {
                    RefillCardPool(() =>
                    {
                    });
                }

                private void OnBattleStartExit()
                {
                }
            #endregion
        #endregion
    }
}