using System.Collections.Generic;
using BATTLE.CARD.BATTLE;
using BATTLE.CARD.STATE.TYPE;
using PLAYER.CUSTOMIZE.BATTLE.SELECTION_ORDER;
using UnityEngine;

namespace BATTLE.SYSTEM.MAIN.TYPE
{
    internal class HandCardSystem : MonoBehaviour
    {
        [field: SerializeField] private SelectionOrderCustomizedData SelectionOrderCustomizedData { get; set; }
        
        private List<BattleCardBase> HandCards { get; set; } = new();
        private List<BattleCardBase> SelectedCards { get; set; } = new();

        private void OnEnable()
        {
            DeselectedState.CanBeSelect += SelectCard;
        }
        
        private void OnDisable()
        {
            DeselectedState.CanBeSelect -= SelectCard;
        }

        private bool SelectCard(BattleCardBase card)
        {
            if (SelectedCards.Count < 3)
            {
                SelectedCards.Add(card);
                RefreshSelectionOrder();
                return true;
            }
            else
            {
                // TODO Show a message to the user indicating that no more cards can be selected.
            }

            return false;
        }

        private void RefreshSelectionOrder()
        {
            foreach (var selectedCard in SelectedCards)
                selectedCard.RemoveSelectionOrder();
            for (var i = 0; i < SelectedCards.Count; i++)
                SelectedCards[i].AddSelectionOrder(SelectionOrderCustomizedData.SelectionOrderPrefabs[i]);
        }
    }
}