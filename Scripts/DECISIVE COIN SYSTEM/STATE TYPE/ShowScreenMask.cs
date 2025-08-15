using DECISIVE_COIN_SYSTEM.STATE_MACHINE;

namespace DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ShowScreenMask : IState
    {
        private DecisiveCoinSystem MainSystem;
        
        public void OnEnter(DecisiveCoinSystem system)
        {
            MainSystem = system;
            MainSystem.ShowScreenMask(() => MainSystem.ReadyToTossCoinState());
        }

        public void OnExit()
        {
        }
    }
}