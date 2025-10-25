namespace System.Restaurant.Main.State_Machine
{
    internal interface IState
    {
        public void Enter();
        public void Exit();
    }
}