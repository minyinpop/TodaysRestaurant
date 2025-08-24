using System;

namespace Battle_Management.State.Type
{
    internal class OnBattleStart : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        
        public OnBattleStart(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
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