using System;

namespace Battle.State_Machine.State
{
    internal sealed class OnBattleStart : IBattleState
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