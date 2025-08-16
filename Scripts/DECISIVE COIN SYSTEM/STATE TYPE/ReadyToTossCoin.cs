using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;

namespace DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ReadyToTossCoin : IState
    {
        private DecisiveCoinSystem MainSystem;
        
        public void OnEnter(DecisiveCoinSystem system)
        {
            MainSystem = system;
            MainSystem.MoveToTossPoint()
                .OnComplete(() => MainSystem.SetCoinInteractToTrue());
        }
    }
}