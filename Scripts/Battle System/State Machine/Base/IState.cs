namespace Battle_System.State_Machine.Base
{
    internal interface IState
    {
        public void Enter();
        public void Exit();
    }
}