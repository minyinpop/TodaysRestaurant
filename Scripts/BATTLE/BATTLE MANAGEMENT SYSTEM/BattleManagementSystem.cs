using BATTLE.BATTLE_MANAGEMENT_SYSTEM.STATE_MACHINE;
using BATTLE.BATTLE_MANAGEMENT_SYSTEM.STATE_TYPE;
using UnityEngine;

namespace BATTLE.BATTLE_MANAGEMENT_SYSTEM
{
    internal class BattleManagementSystem : MonoBehaviour
    {
        public static event System.Action<System.Action> OnEnterRefillCardPoolState;
        public static event System.Action<System.Action> OnDrawCardAndShowWhenStartBattleState;
        
        private readonly StateMachine StateMachine = new();
        
        private void Start()
        {
            RefillCardPoolState();
        }
        
        #region State Machine
            private void ChangeState(IState newState) => StateMachine.ChangeState(newState);

            #region Refill Card Pool State
                private void RefillCardPoolState() => ChangeState(new RefillCardPool(() =>
                    OnEnterRefillCardPoolState?.Invoke(OnRefillCardPoolStateComplete)));

                private void OnRefillCardPoolStateComplete()
                {
                    DrawCardAndShowWhenStartBattleState();
                }
            #endregion

            #region Draw Card And Show When Start Battle State
                private void DrawCardAndShowWhenStartBattleState() =>ChangeState(new DrawCardWhenStartBattle(() =>
                    OnDrawCardAndShowWhenStartBattleState?.Invoke(OnDrawCardAndShowWhenStartBattleStateComplete)));

                private void OnDrawCardAndShowWhenStartBattleStateComplete()
                {
                    Debug.Log("Draw Card Pool State Complete");
                }
            #endregion
        #endregion
    }
}