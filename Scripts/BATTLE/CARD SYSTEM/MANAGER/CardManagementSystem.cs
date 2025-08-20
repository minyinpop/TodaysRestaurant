using System.Collections;
using BATTLE.BATTLE_MANAGEMENT_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.HAND_CARD_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.SHOW_CARD_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER
{
    internal class CardManagementSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;
        
        private IEnumerator GetCardFromCardPoolCoroutine;

        private void OnEnable()
        {
            BattleManagementSystem.OnEnterRefillCardPoolState += RefillCardPool;
            BattleManagementSystem.OnDrawCardAndShowWhenStartBattleState += DrawCardAndShowWhenStartBattle;
        }

        private void OnDisable()
        {
            BattleManagementSystem.OnEnterRefillCardPoolState -= RefillCardPool;
            BattleManagementSystem.OnDrawCardAndShowWhenStartBattleState -= DrawCardAndShowWhenStartBattle;

            if (GetCardFromCardPoolCoroutine is not null)
            {
                StopCoroutine(GetCardFromCardPoolCoroutine);
                GetCardFromCardPoolCoroutine = null;
            }
        }
        
        #region Card Pool System
            private void RefillCardPool(System.Action OnComplete) => CardPoolSystem.RefillCardPool(OnComplete);
        #endregion
        
        #region Draw Card System
            private void DrawCardAndShowWhenStartBattle(System.Action OnComplete)
            {
                GetCardFromCardPoolCoroutine = DrawCardAndShowWhenStartBattleCoroutine(OnComplete);
                StartCoroutine(GetCardFromCardPoolCoroutine);
            }
            
            private IEnumerator DrawCardAndShowWhenStartBattleCoroutine(System.Action OnComplete)
            {
                for (var i = 0; i < 5; i++)
                {
                    CardPoolSystem.GetCard(out var card);
                    ShowCardSystem.DrawCardWhenStartBattle(card);
                    yield return new WaitForSeconds(.2f);
                }

                OnComplete?.Invoke();
            }
        #endregion
    }
}