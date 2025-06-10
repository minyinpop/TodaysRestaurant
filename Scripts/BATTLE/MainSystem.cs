using BATTLE.STATE_MACHINE.BASE;
using BATTLE.STATE_MACHINE.CATEGORY;
using BATTLE.SYSTEM.BATTLE;
using BATTLE.SYSTEM.CARD;
using UnityEngine;

namespace BATTLE
{
    [RequireComponent(typeof(BattleSystem))]
    [RequireComponent(typeof(CardSystem))]
    internal class MainSystem : MonoBehaviour
    {
        private BattleStateMachine BattleStateMachine { get; set; }
        
        private BattleSystem BattleSystem { get; set; }
        private CardSystem CardSystem { get; set; }

        private void Awake()
        {
            BattleStateMachine = new BattleStateMachine();
            BattleStateMachine.ChangeState(new InitiativeSelectionState());
            
            BattleSystem = GetComponent<BattleSystem>();
            CardSystem = GetComponent<CardSystem>();
        }
    }
}