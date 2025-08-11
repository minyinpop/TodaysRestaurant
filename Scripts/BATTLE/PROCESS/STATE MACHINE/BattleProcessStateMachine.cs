namespace BATTLE.PROCESS.STATE_MACHINE
{
    internal class BattleProcessStateMachine
    {
        private IBattleProcessState CurrentState;

        public void ChangeState(BattleProcessSystem system, IBattleProcessState newState)
        {
            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter(system);
        }
    }
}