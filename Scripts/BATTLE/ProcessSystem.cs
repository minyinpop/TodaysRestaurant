using BATTLE.STATE_MACHINE;
using BATTLE.STATE_MACHINE.STATE;
using BATTLE.SYSTEM;
using UnityEngine;

namespace BATTLE
{
    /// <summary>
    /// 主要是用來管理整個戰鬥進程的程式碼
    /// 使用狀態機來區分各種不同的狀態
    /// 有各種公開的方法給狀態機裡的狀態做調用
    /// </summary>
    [RequireComponent(typeof(SelectionInitiativeSystem))]
    [RequireComponent(typeof(ScreenMaskSystem))]
    internal class ProcessSystem : MonoBehaviour
    {
        /// <summary>
        /// 用來暫存狀態機
        /// </summary>
        private ProcessStateMachine ProcessStateMachine = new();
        
        private SelectionInitiativeSystem SelectionInitiativeSystem;
        private ScreenMaskSystem ScreenMaskSystem;

        private void Awake()
        {
            // TODO 開發用，未來改回 new OnStart()
            ProcessStateMachine.ChangeState(this, new OnSelectionInitiative());
            
            SelectionInitiativeSystem = GetComponent<SelectionInitiativeSystem>();
            ScreenMaskSystem = GetComponent<ScreenMaskSystem>();
        }

        /// <summary>
        /// 用於切換狀態機的狀態
        /// </summary>
        /// <param name="newState"> 下一個狀態 </param>
        public void ChangeState(IProcessState newState) => ProcessStateMachine.ChangeState(this, newState);
        
        
        
        #region Selection Initiative System Method
        /// <summary>
        /// 用來生成硬幣的方法
        /// </summary>
        public void SpawnCoin() => SelectionInitiativeSystem.SpawnCoin();
        #endregion

        
        
        #region Screen Mask System Method
        /// <summary>
        /// 用來顯示遮罩的方法
        /// </summary>
        public void ShowMask() => ScreenMaskSystem.ShowMask();
        /// <summary>
        /// 用來隱藏遮罩的方法
        /// </summary>
        public void HideMask() => ScreenMaskSystem.HideMask();
        #endregion
    }
}