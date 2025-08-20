using BATTLE.BATTLE_MANAGEMENT_SYSTEM.STATE_MACHINE;

namespace BATTLE.BATTLE_MANAGEMENT_SYSTEM.STATE_TYPE
{
    internal class RefillCardPool : IState
    {
        private readonly System.Action onEnter;
        public RefillCardPool(System.Action OnEnter) => onEnter = OnEnter;
        public void Enter() => onEnter?.Invoke();
    }
}