using BATTLE.SYSTEM.MAIN.TYPE;
using BATTLE.SYSTEM.STATE;
using BATTLE.SYSTEM.STATE.TYPE;
using UnityEngine;

namespace BATTLE.SYSTEM.MAIN
{
    [RequireComponent(typeof(InitiativeSelectionSystem))]
    [RequireComponent(typeof(HandCardSystem))]
    internal class MainSystem : MonoBehaviour
    {
        private BattleStateMachine StateMachine { get; set; } = new();
        
        // Child System
        private InitiativeSelectionSystem InitiativeSelectionSystem { get; set; }
        private HandCardSystem HandCardSystem { get; set; }

        private void Awake()
        {
            InitiativeSelectionSystem = GetComponent<InitiativeSelectionSystem>();
            HandCardSystem = GetComponent<HandCardSystem>();
        }

        private void Start()
        {
            StateMachine.ChangeState(new InitiativeSelectionState());
        }

        private void OnEnable()
        {
            InitiativeSelectionState.EnterEvent += OnEnterInitiativeSelectionState;
        }
        
        private void OnDisable()
        {
            InitiativeSelectionState.EnterEvent -= OnEnterInitiativeSelectionState;
        }
        
        private void OnEnterInitiativeSelectionState()
        {
            InitiativeSelectionSystem.Init();
        }
    }
}