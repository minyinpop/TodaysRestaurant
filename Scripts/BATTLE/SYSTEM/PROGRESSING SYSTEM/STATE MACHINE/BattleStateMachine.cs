namespace BATTLE.SYSTEM.PROGRESSING_SYSTEM.STATE_MACHINE
{
    internal class BattleStateMachine
    {
        private IBattleState CurrentState;

        /// <summary>
        /// 退出當前的狀態
        /// 進到下一個狀態
        /// </summary>
        /// <param name="newState"> 下一個狀態 </param>
        public void ChangeState(IBattleState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}