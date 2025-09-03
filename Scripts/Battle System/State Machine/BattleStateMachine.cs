namespace Battle_System.State_Machine
{
    internal class BattleStateMachine
    {
        private IBattleState CurrentState;

        public void ChangeState(IBattleState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}