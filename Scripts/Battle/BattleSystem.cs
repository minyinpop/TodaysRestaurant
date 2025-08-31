using Battle.Child_System;
using Battle.State_Machine;
using Battle.State_Machine.State;
using UnityEngine;

namespace Battle
{
    internal sealed class BattleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private DrawCardSystem DrawCardSystem;

        private readonly BattleStateMachine BattleStateMachine = new();

        private void Start()
        {
            OnBattleStart();
        }

        private void OnBattleStart()
        {
            BattleStateMachine.ChangeState(new OnBattleStart(OnBattleStartEnter, OnBattleStartExit));
        }

        private void OnBattleStartEnter()
        {
            CardPoolSystem.Refill(() =>
            {
                Debug.Log("Refill Complete.");
            });
        }

        private void OnBattleStartExit()
        {
        }
    }
}