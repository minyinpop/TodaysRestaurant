using BATTLE.BATTLE_MANAGEMENT_SYSTEM.STATE_MACHINE;
using BATTLE.BATTLE_MANAGEMENT_SYSTEM.STATE_TYPE;
using UnityEngine;

namespace BATTLE.BATTLE_MANAGEMENT_SYSTEM
{
    internal class BattleManagementSystem : MonoBehaviour
    {
        public static event System.Action<System.Action> OnEnterRefillCardPoolState;
        
        private readonly StateMachine StateMachine = new();
        
        private void Start()
        {
            RefillCardPoolState();
        }
        
        #region State Machine
            private void ChangeState(IState newState) => StateMachine.ChangeState(newState);
            private void RefillCardPoolState() => ChangeState(new RefillCardPool(() => OnEnterRefillCardPoolState?.Invoke(() =>
            {
                Debug.Log("Refill Card Pool Complete.");
            })));
        #endregion
    }
}