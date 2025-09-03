using System;

namespace Battle_System.State_Machine.State
{
    internal class OnBattleStart : IBattleState
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
            OnEnter();
        }
        
        public void Exit()
        {
            OnExit();
        }
    }
}