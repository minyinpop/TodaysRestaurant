using BATTLE.SYSTEM.MAIN.TYPE;
using BATTLE.SYSTEM.STATE;
using BATTLE.SYSTEM.STATE.TYPE;
using UnityEngine;

namespace BATTLE.SYSTEM.MAIN
{
    [RequireComponent(typeof(InitiativeSelectionSystem))]
    internal class MainSystem : MonoBehaviour
    {
        private BattleStateMachine StateMachine { get; set; } = new();
        
        // Child System
        private InitiativeSelectionSystem InitiativeSelectionSystem { get; set; }

        private void Awake()
        {
            InitiativeSelectionSystem = GetComponent<InitiativeSelectionSystem>();
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