using BATTLE.SYSTEM.INITIATIVE;
using BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE;
using BATTLE.SYSTEM.PROCESS.STATE_MACHINE;
using BATTLE.SYSTEM.PROCESS.STATE_MACHINE.STATE;
using BATTLE.SYSTEM.SCREEN_MASK;
using UnityEngine;

namespace BATTLE.SYSTEM
{
    [RequireComponent(typeof(InitiativeSystem))]
    [RequireComponent(typeof(ScreenMaskSystem))]
    internal class ProcessSystem : MonoBehaviour
    {
        private InitiativeSystem InitiativeSystem;
        private ScreenMaskSystem ScreenMaskSystem;

        private readonly ProcessStateMachine StateMachine = new();
        
        private void Awake()
        {
            InitiativeSystem = GetComponent<InitiativeSystem>();
            ScreenMaskSystem = GetComponent<ScreenMaskSystem>();

            ChangeState(new FocusOnEnemy());
        }

        public void ChangeState(IProcessState newState) => StateMachine.ChangeState(this, newState);
        
        #region Initiative System
            public void ChangeState(IInitiativeState newState) => InitiativeSystem.ChangeState(newState);
        #endregion
        
        #region Screen Mask System
            public void ShowScreenMask(System.Action onComplete = null) => ScreenMaskSystem.Show(onComplete);
            public void HideScreenMask(System.Action onComplete = null) => InitiativeSystem.OnTossResultShowFinish += () => ScreenMaskSystem.Hide(onComplete);
            // TODO Continue to coding that what will happen when ScreenMask back to 0 Alpha.
        #endregion
    }
}