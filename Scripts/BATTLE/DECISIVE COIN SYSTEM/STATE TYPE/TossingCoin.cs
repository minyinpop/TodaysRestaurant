using BATTLE.DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;

namespace BATTLE.DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class TossingCoin : IState
    {
        private readonly System.Action onComplete;
        public TossingCoin(System.Action OnComplete) => onComplete = OnComplete;
        
        #region Interface
            public void OnEnter(DecisiveCoinSystem system)
            {
                system.TossingCoin()
                    .OnComplete(() => onComplete?.Invoke());
            }
        #endregion
    }
}