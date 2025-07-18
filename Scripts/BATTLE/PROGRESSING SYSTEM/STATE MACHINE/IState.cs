namespace BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE
{
    internal interface IState
    {
        /// <summary>
        /// 用於執行進入狀態後的程式
        /// </summary>
        /// <param name="system"> 執行戰鬥的進程系統 </param>
        public void Enter(ProgressingSystem system);
        
        /// <summary>
        /// 用於執行離開狀態後的程式
        /// </summary>
        public void Exit();
    }
}