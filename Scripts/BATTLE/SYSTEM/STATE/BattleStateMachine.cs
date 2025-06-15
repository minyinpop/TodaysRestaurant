namespace BATTLE.SYSTEM.STATE
{
    internal class BattleStateMachine
    {
        private IBattleState CurrentState { get; set; }

        public void ChangeState(IBattleState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}