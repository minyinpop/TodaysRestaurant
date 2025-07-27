namespace BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE
{
    internal class InitiativeStateMachine
    {
        private IInitiativeState CurrentState;

        public void ChangeState(InitiativeSystem system, IInitiativeState newState)
        {
            CurrentState = newState;
            CurrentState?.Enter(system);
        }
    }
}