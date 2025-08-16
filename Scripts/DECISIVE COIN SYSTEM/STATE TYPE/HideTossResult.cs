using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;

namespace DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class HideTossResult : IState
    {
        private readonly System.Action onComplete;
        public HideTossResult(System.Action OnComplete) => onComplete = OnComplete;
        
        #region Interface
            public void OnEnter(DecisiveCoinSystem system)
            {
                DOTween.Sequence()
                    .Append(system.HideCoin())
                    .Join(system.HideTossResultText())
                    .Append(system.HideScreenMask())
                    .OnComplete(() => onComplete?.Invoke());
            }
        #endregion
    }
}