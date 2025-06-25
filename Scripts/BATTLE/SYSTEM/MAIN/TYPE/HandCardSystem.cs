using System.Collections.Generic;
using BATTLE.CARD.BATTLE;
using PLAYER.CUSTOMIZE.BATTLE.SELECTION_ORDER;
using UnityEngine;

namespace BATTLE.SYSTEM.MAIN.TYPE
{
    internal class HandCardSystem : MonoBehaviour
    {
        [field: SerializeField] private SelectionOrderCustomizedData SelectionOrderCustomizedData { get; set; }
        
        private List<BattleCardBase> HandCards { get; set; } = new();
        private List<BattleCardBase> SelectedCards { get; set; } = new() { null, null, null };
        
        private void OnEnable()
        {
            BattleCardBase.SelectedEvent += OnBattleCardSelected;
            BattleCardBase.DeselectEvent += OnBattleCardDeselected;
        }

        private void OnDisable()
        {
            BattleCardBase.SelectedEvent -= OnBattleCardSelected;
            BattleCardBase.DeselectEvent -= OnBattleCardDeselected;
        }

        private GameObject OnBattleCardSelected(BattleCardBase selectedCard)
        {
            // TODO Change 3 to character in the future.
            
            for (var i = 0; i < 3; i++)
            {
                if (SelectedCards[i] is not null) continue;
                SelectedCards[i] = selectedCard;
                return SelectionOrderCustomizedData.SelectionOrderPrefabs[i];
            }

            return null;
        }

        private void OnBattleCardDeselected(BattleCardBase deselectedCard)
        {
            Debug.Log("EWE");
        }
    }
}