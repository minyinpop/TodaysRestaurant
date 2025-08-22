using System;
using Battle_Management_System.State_Machine;

namespace Battle_Management_System.State_Type
{
    internal class OnRefillCardWhenBattleStart : IState
    {
        private readonly Action OnEnter;
        public OnRefillCardWhenBattleStart(Action onEnter) => OnEnter = onEnter;
        public void Enter() => OnEnter?.Invoke();
    }
}