namespace Restaurant_System.Object.Cookware.System.State_Machine
{
    internal sealed class StateMachine
    {
        private IState _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void InteractState()
        {
            _currentState?.Interact();
        }
    }
}