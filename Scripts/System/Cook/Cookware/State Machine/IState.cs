namespace System.Cook.Cookware.State_Machine
{
    internal interface IState
    {
        public void Enter();
        public void Exit();
    }
}