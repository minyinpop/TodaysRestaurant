using BATTLE.DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;

namespace BATTLE.DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ShowTossResult : IState
    {
        private readonly System.Action onComplete;
        public ShowTossResult(System.Action OnComplete) => onComplete = OnComplete;
        
        #region Interface
            public void OnEnter(DecisiveCoinSystem system)
            {
                DOTween.Sequence()
                    .Append(system.MoveToShowPoint())
                    .AppendInterval(.5f)
                    .Append(system.ShowTossResultText())
                    .AppendInterval(2)
                    .OnComplete(() => onComplete?.Invoke());
            }
        #endregion
    }
}