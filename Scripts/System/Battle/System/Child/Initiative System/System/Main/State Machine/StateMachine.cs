namespace System.Battle.System.Child.Initiative_System.System.Main.State_Machine
{
    internal sealed class StateMachine
    {
        private IState CurrentState;
        
        /// <summary>
        /// 退出狀態後進到新傳入的狀態。
        /// </summary>
        /// <param name="nextState">下一個狀態</param>
        public void ChangeState(IState nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState?.Enter();
        }
    }
}