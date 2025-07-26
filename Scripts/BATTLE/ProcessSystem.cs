using BATTLE.STATE_MACHINE;
using BATTLE.STATE_MACHINE.STATE;
using BATTLE.SYSTEM;
using UnityEngine;

namespace BATTLE
{
    [RequireComponent(typeof(InitiativeSystem))]
    internal class ProcessSystem : MonoBehaviour
    {
        private InitiativeSystem InitiativeSystem;
        
        private ProcessStateMachine StateMachine = new();

        private void Awake()
        {
            InitiativeSystem = GetComponent<InitiativeSystem>();
            
            StateMachine.ChangeState(new OnStart());
        }
    }
}