namespace BATTLE.STATE_MACHINE
{
    /// <summary>
    /// 戰鬥進程的狀態，用作於各狀態的接口
    /// </summary>
    internal interface IProcessState
    {
        public void Enter(ProcessSystem system);
        public void Exit();
    }
}