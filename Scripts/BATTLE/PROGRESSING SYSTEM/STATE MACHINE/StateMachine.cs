namespace BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE
{
    internal class StateMachine
    {
        /// <summary>
        /// 當前的狀態
        /// </summary>
        private IState CurrentState;

        /// <summary>
        /// 離開當前狀態，並進入下個狀態
        /// </summary>
        /// <param name="system"> 執行戰鬥的進程系統 </param>
        /// <param name="newState"> 下一個狀態 </param>
        public void ChangeState(ProgressingSystem system, IState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter(system);
        }
    }
}