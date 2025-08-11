namespace BATTLE.PROCESS.STATE_MACHINE
{
    internal interface IBattleProcessState
    {
        public void OnEnter(BattleProcessSystem system);
        public void OnExit();
    }
}