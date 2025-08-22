using Battle_Management_System.Decisive_Coin_System.State_Machine;
using DG.Tweening;

namespace Battle_Management_System.Decisive_Coin_System.State_Type
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