using DECISIVE_COIN_SYSTEM.STATE_MACHINE;

namespace DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ReadyToTossCoin : IState
    {
        private DecisiveCoinSystem DecisiveCoinSystem;
        
        public void OnEnter(DecisiveCoinSystem system)
        {
            DecisiveCoinSystem = system;
            DecisiveCoinSystem.MoveToTossPoint();
        }

        public void OnExit()
        {
        }
    }
}