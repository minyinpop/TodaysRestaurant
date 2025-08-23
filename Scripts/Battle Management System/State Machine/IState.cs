namespace Battle_Management_System.State_Machine
{
    internal interface IState
    {
        public void OnEnter();
        public void OnExit();
    }
}