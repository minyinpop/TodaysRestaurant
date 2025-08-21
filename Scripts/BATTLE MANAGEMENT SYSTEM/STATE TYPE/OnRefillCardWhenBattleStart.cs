using System;
using BATTLE_MANAGEMENT_SYSTEM.STATE_MACHINE;

namespace BATTLE_MANAGEMENT_SYSTEM.STATE_TYPE
{
    internal class OnRefillCardWhenBattleStart : IState
    {
        private readonly Action OnEnter;
        public OnRefillCardWhenBattleStart(Action onEnter) => OnEnter = onEnter;
        
        public void Enter()
        {
        }
    }
}