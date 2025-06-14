using BATTLE.STATE_MACHINE.BASE;

namespace BATTLE.STATE_MACHINE.CATEGORY
{
    internal class InitiativeSelectionState : IBattleState
    {
        public static event System.Action OnEnterEvent;
        
        public void Enter()
        {
            OnEnterEvent?.Invoke();
        }

        public void Exit()
        {
            
        }
    }
}