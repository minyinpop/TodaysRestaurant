namespace Battle.State_Machine
{
    internal sealed class BattleStateMachine
    {
        private IBattleState CurrentBattleState;

        public void ChangeState(IBattleState NewBattleState)
        {
            NewBattleState?.Exit();
            CurrentBattleState = NewBattleState;
            NewBattleState?.Enter();
        }
    }
}