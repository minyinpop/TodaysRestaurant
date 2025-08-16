using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;

namespace DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ShowScreenMask : IState
    {
        private DecisiveCoinSystem MainSystem;
        
        public void OnEnter(DecisiveCoinSystem system)
        {
            MainSystem = system;
            MainSystem.ShowScreenMask()
                .OnComplete(() => MainSystem.ReadyToTossCoinState());
        }
    }
}