using BATTLE.DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;

namespace BATTLE.DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ReadyToTossCoin : IState
    {
        private readonly System.Action onComplete;
        public ReadyToTossCoin(System.Action OnComplete) => onComplete = OnComplete;
        
        #region Interface
            public void OnEnter(DecisiveCoinSystem system)
            {
                system.MoveToTossPoint()
                    .OnComplete(() => onComplete?.Invoke());
            }
        #endregion
    }
}