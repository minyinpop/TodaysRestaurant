namespace Restaurant_System.Object.Creature.Customer.System.Main.State_Machine
{
    internal sealed class StateMachine
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