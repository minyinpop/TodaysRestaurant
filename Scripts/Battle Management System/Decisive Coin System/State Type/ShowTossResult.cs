using Battle_Management_System.Decisive_Coin_System.State_Machine;
using DG.Tweening;

namespace Battle_Management_System.Decisive_Coin_System.State_Type
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