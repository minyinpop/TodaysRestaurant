using System.Collections.Generic;
using BATTLE.CARD;
using BATTLE.CARD.STATE_MACHINE.STATE;
using DATA.SELECTION_ORDER;
using UnityEngine;

namespace BATTLE.SYSTEM.CHILD_SYSTEM
{
    internal class BattleCardSystem : MonoBehaviour
    {
        // [field: Header("卡片選擇順序的資料")]
        [field: SerializeField] private SelectionOrderSO SelectionOrderSO;
        
        /// <summary>
        /// 用於暫存手上的戰鬥卡片
        /// </summary>
        private List<CardBase> HandCards = new();
        /// <summary>
        /// 用於暫存手上且被選擇的戰鬥卡片
        /// </summary>
        private List<CardBase> SelectedCards = new();

        private void OnEnable()
        {
            DeselectedState.Select += RENAME;
        }

        private void OnDisable()
        {
            DeselectedState.Select -= RENAME;
        }
        
        
        
        private bool RENAME(CardBase card)
        {
            // TODO 未來的 3 可以改成依照玩家場上還可行動的 NPC，來決定一回合可以使用多少張的戰鬥卡片
            if (SelectedCards.Count < 3)
            {
                SelectedCards.Add(card);
                card.SetSelectionOrder(SelectionOrderSO.Prefabs[SelectedCards.IndexOf(card)]);
                return true;
            }

            return false;
        }
    }
}