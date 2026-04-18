namespace Common.Enemy_Explore_Object.State_Machine
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public void Exit();
    }
}