namespace BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE
{
    internal class StateMachine
    {
        private IState CurrentState;
        
        public void ChangeState(IState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}