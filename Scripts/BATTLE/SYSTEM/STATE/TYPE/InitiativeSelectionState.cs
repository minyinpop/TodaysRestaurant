namespace BATTLE.SYSTEM.STATE.TYPE
{
    internal class InitiativeSelectionState : IBattleState
    {
        public static event System.Action EnterEvent;

        public void Enter()
        {
            EnterEvent?.Invoke();
        }

        public void Exit()
        {
            
        }
    }
}