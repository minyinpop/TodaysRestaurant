using Battle_System.Child_System;
using Battle_System.State_Machine;
using Battle_System.State_Machine.State;
using UnityEngine;

namespace Battle_System
{
    internal sealed class BattleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private DrawCardSystem DrawCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;

        private readonly BattleStateMachine StateMachine = new();

        private void Start()
        {
            OnBattleStart();
        }

        private void OnBattleStart()
        {
            StateMachine.ChangeState(new OnBattleStart(OnBattleStartEnter, OnBattleStartExit));
        }

        private void OnBattleStartEnter()
        {
            CardPoolSystem.Refill();
        }
        
        private void OnBattleStartExit()
        {
        }
    }
}