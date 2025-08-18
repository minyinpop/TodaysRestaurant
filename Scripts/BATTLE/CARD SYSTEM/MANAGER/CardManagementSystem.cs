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

        private void Awake()
        {
            CardPoolSystem.OnRefillCardPoolComplete += TEST;
        }
        
        public void Start()
        {
            // For Development Only.
            RefillCardPool();
        }

        private void TEST()
        {
            // TODO 當卡片生成完畢後，就要抽卡並展示，等待完成。
            Debug.Log("On Refill Card Pool Complete.");
        }
        
        #region Card Pool System
            private void RefillCardPool() => CardPoolSystem.RefillCardPool();
        #endregion
    }
}