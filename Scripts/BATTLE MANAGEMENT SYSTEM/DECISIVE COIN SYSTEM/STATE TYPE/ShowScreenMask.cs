using BATTLE_MANAGEMENT_SYSTEM.DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;

namespace BATTLE_MANAGEMENT_SYSTEM.DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ShowScreenMask : IState
    {
        private readonly System.Action onComplete;
        public ShowScreenMask(System.Action OnComplete) => onComplete = OnComplete;

        #region Interface
            public void OnEnter(DecisiveCoinSystem system)
            {
                system.ShowScreenMask()
                    .OnComplete(() => onComplete?.Invoke());
            }
        #endregion
    }
}