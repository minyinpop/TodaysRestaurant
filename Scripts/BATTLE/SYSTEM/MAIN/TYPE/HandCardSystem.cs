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
        private List<BattleCardBase> SelectedCards { get; set; } = new();
    }
}