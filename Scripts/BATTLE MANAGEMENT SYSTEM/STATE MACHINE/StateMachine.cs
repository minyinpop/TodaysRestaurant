namespace BATTLE_MANAGEMENT_SYSTEM.STATE_MACHINE
{
    internal class StateMachine
    {
        private IState CurrentState;

        public void ChangeState(IState newState)
        {
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}