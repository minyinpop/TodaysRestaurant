namespace BATTLE.STATE_MACHINE.BASE
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