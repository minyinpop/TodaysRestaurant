using BATTLE.PROCESS_SYSTEM.STATE_MACHINE;
using BATTLE.PROCESS_SYSTEM.STATE_TYPE;
using BATTLE.SCREEN_MASK_SYSTEM;
using UnityEngine;

namespace BATTLE.PROCESS_SYSTEM
{
    [RequireComponent(typeof(ScreenMaskSystem))]
    internal class BattleProcessSystem : MonoBehaviour
    {
        private ScreenMaskSystem ScreenMaskSystem;

        private readonly BattleProcessStateMachine StateMachine = new();

        private void Awake()
        {
            ScreenMaskSystem = GetComponent<ScreenMaskSystem>();
        }

        private void Start()
        {
            // TODO 【2025.08.12 00:01】 Change the state in future, just for develop now.
            ChangeStateToShowScreenMask();
        }

        #region State Machine
            private void ChangeState(IBattleProcessState newState) => StateMachine.ChangeState(this, newState);
            public void ChangeStateToShowScreenMask() => ChangeState(new ShowScreenMask());
            public void ChangeStateToSpawnInitiativeCoinAndReadyToToss() => ChangeState(new SpawnInitiativeCoinAndReadyToToss());
        #endregion
        
        #region Screen Mask System
            public void ShowScreenMask(System.Action onComplete = null) => ScreenMaskSystem.Show(onComplete);
            public void HideScreenMask(System.Action onComplete = null) => ScreenMaskSystem.Hide(onComplete);
        #endregion
    }
}