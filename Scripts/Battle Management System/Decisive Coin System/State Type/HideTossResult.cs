using Battle_Management_System.Decisive_Coin_System.State_Machine;
using DG.Tweening;

namespace Battle_Management_System.Decisive_Coin_System.State_Type
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