using Battle_Management_System.Card_Pool_System;
using Battle_Management_System.State_Machine;
using Battle_Management_System.State_Type;
using UnityEngine;

namespace Battle_Management_System
{
    internal class BattleManagementSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;

        private readonly StateMachine StateMachine = new();

        private void Awake()
        {
            OnRefillCardWhenBattleStart();
        }
        
        #region State Machine
            private void ChangeState(IState newState) => StateMachine.ChangeState(newState);
            private void OnRefillCardWhenBattleStart() => ChangeState(new OnRefillCardWhenBattleStart(RefillCard));
        #endregion
        
        #region Card Pool System
            private void RefillCard() => CardPoolSystem.RefillCard();
        #endregion
    }
}