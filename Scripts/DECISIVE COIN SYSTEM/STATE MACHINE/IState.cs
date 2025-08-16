namespace DECISIVE_COIN_SYSTEM.STATE_MACHINE
{
    internal interface IState
    {
        public void OnEnter(DecisiveCoinSystem system);
    }
}