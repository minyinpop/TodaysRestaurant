using BATTLE.BATTLE_MANAGEMENT_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.DRAW_CARD_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.HAND_CARD_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER
{
    internal class CardManagementSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private DrawCardSystem DrawCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;

        private void OnEnable()
        {
            BattleManagementSystem.OnEnterRefillCardPoolState += RefillCardPool;
        }

        private void OnDisable()
        {
            BattleManagementSystem.OnEnterRefillCardPoolState -= RefillCardPool;
        }
        
        #region Card Pool System
            private void RefillCardPool(System.Action OnComplete) => CardPoolSystem.RefillCardPool(OnComplete);
        #endregion
    }
}