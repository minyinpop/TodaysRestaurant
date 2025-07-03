namespace BATTLE.SYSTEM.PROGRESSING_SYSTEM.STATE_MACHINE
{
    internal interface IBattleState
    {
        public void Enter();
        public void Exit();
    }
}