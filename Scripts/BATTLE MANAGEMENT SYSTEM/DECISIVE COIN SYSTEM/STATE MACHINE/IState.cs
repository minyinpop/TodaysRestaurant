namespace BATTLE_MANAGEMENT_SYSTEM.DECISIVE_COIN_SYSTEM.STATE_MACHINE
{
    internal interface IState
    {
        public void OnEnter(DecisiveCoinSystem system);
    }
}