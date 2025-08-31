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

        private const int OnBattleStartDrawNumber = 6;

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
                CardPoolSystem.GetAllSlots(out var CardPoolSlots);
                DrawCardSystem.DrawCardFromCardPool(CardPoolSlots, OnBattleStartDrawNumber, () =>
                {
                    Debug.Log("Draw Card Complete.");
                });
            });
        }

        private void OnBattleStartExit()
        {
        }
    }
}