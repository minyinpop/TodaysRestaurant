namespace BATTLE.STATE_MACHINE
{
    /// <summary>
    /// 戰鬥進程的狀態機，用來區分當前的進度
    /// 「初始化戰鬥」、「決定哪方先行動」、「玩家的回合」、「敵人的回合」以及「戰鬥結算」
    /// </summary>
    internal class ProcessStateMachine
    {
        /// <summary>
        /// 用來儲存當前的狀態
        /// </summary>
        private IProcessState CurrentState;

        /// <summary>
        /// 會先執行舊狀態的「退出」，再執行新狀態的「進入」
        /// </summary>
        /// <param name="system"> 用來管理戰鬥進程的系統 </param>
        /// <param name="newState"> 下一個狀態 </param>
        public void ChangeState(ProcessSystem  system, IProcessState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter(system);
        }
    }
}