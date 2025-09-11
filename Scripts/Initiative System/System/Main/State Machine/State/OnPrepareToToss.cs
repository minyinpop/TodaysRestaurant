using System;

namespace Initiative_System.System.Main.State_Machine.State
{
    internal sealed class OnPrepareToToss : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        /// <summary>
        /// 設定各狀態所執行的方法
        /// </summary>
        /// <param name="onEnter">進入時所執行的方法</param>
        /// <param name="onExit">離開時所執行的方法</param>
        public OnPrepareToToss(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }

        /// <summary>
        /// 進入時所執行的方法
        /// </summary>
        public void Enter()
        {
            OnEnter?.Invoke();
        }

        /// <summary>
        /// 離開時所執行的方法
        /// </summary>
        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}