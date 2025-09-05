using System;

namespace Card_Battle_System.System.Main.State_Machine.State
{
    internal sealed class OnBattleStart : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnBattleStart(Action OnEnter, Action OnExit)
        {
            this.OnEnter = OnEnter;
            this.OnExit = OnExit;
        }

        public void Enter()
        {
            OnEnter?.Invoke();
        }
        
        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}