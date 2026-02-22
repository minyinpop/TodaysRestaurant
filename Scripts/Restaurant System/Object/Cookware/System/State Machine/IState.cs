namespace Restaurant_System.Object.Cookware.System.State_Machine
{
    internal interface IState
    {
        public void Enter();
        public void Exit();
        
        public void Interact();
    }
}