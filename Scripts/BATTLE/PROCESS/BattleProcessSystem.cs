using BATTLE.PROCESS.STATE_MACHINE;
using BATTLE.PROCESS.STATE_TYPE;
using BATTLE.SCREEN_MASK;
using UnityEngine;

namespace BATTLE.PROCESS
{
    [RequireComponent(typeof(ScreenMaskSystem))]
    internal class BattleProcessSystem : MonoBehaviour
    {
        private ScreenMaskSystem ScreenMaskSystem;

        private BattleProcessStateMachine StateMachine = new();

        private void Awake()
        {
            ScreenMaskSystem = GetComponent<ScreenMaskSystem>();
        }

        private void Start()
        {
            // TODO 【2025.08.12 00:01】 Change the state in future, just for develop now.
            ChangeState(new ShowScreenMask());
        }

        public void ChangeState(IBattleProcessState newState) => StateMachine.ChangeState(this, newState);
        
        #region Screen Mask System
            public void ShowScreenMask(System.Action onComplete = null) => ScreenMaskSystem.Show(onComplete);
            public void HideScreenMask(System.Action onComplete = null) => ScreenMaskSystem.Hide(onComplete);
        #endregion
    }
}