using BATTLE_MANAGEMENT_SYSTEM.CARD_POOL_SYSTEM;
using BATTLE_MANAGEMENT_SYSTEM.STATE_MACHINE;
using BATTLE_MANAGEMENT_SYSTEM.STATE_TYPE;
using UnityEngine;

namespace BATTLE_MANAGEMENT_SYSTEM
{
    [RequireComponent(typeof(CardPoolSystem))]
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
            private void OnRefillCardWhenBattleStart() => ChangeState(new OnRefillCardWhenBattleStart(() => { }));
        #endregion
        
        #region Card Pool System
        #endregion
    }
}