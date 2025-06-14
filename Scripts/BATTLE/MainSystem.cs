using BATTLE.STATE_MACHINE.BASE;
using BATTLE.STATE_MACHINE.CATEGORY;
using BATTLE.SYSTEM.BATTLE;
using BATTLE.SYSTEM.CARD;
using BATTLE.SYSTEM.SELECTION;
using UnityEngine;

namespace BATTLE
{
    [RequireComponent(typeof(InitiativeSelectionSystem))]
    [RequireComponent(typeof(BattleSystem))]
    [RequireComponent(typeof(CardSystem))]
    internal class MainSystem : MonoBehaviour
    {
        private BattleStateMachine BattleStateMachine { get; set; }
        
        private InitiativeSelectionSystem InitiativeSelectionSystem { get; set; }
        private BattleSystem BattleSystem { get; set; }
        private CardSystem CardSystem { get; set; }

        private void Awake()
        {
            BattleStateMachine = new BattleStateMachine();
            BattleStateMachine.ChangeState(new InitiativeSelectionState());
            
            InitiativeSelectionSystem = GetComponent<InitiativeSelectionSystem>();
            BattleSystem = GetComponent<BattleSystem>();
            CardSystem = GetComponent<CardSystem>();
        }
    }
}