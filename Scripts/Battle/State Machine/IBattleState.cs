namespace Battle.State_Machine
{
    internal interface IBattleState
    {
        public void Enter();
        public void Exit();
    }
}