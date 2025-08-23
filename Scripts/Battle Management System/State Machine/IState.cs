namespace Battle_Management_System.State_Machine
{
    internal interface IState
    {
        public void Enter();
        public void Exit();
    }
}