namespace BATTLE.PROCESS_SYSTEM.STATE_MACHINE
{
    internal interface IBattleProcessState
    {
        public void OnEnter(BattleProcessSystem system);
        public void OnExit();
    }
}