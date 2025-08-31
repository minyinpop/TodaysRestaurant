using System;
using Battle_System.State_Machine.Base;

namespace Battle_System.State_Machine.Type
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