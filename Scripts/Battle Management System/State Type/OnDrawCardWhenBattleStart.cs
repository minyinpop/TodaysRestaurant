using System;
using Battle_Management_System.State_Machine;

namespace Battle_Management_System.State_Type
{
    internal class OnDrawCardWhenBattleStart : IState
    {
        private readonly Action OnEnter;
        private readonly Action OnExit;
        public OnDrawCardWhenBattleStart(Action onEnter, Action onExit)
        {
            OnEnter = onEnter;
            OnExit = onExit;
        }
        public void Enter() => OnEnter?.Invoke();
        public void Exit() => OnExit?.Invoke();
    }
}