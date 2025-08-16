using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DG.Tweening;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class HideTossResult : IState
    {
        private DecisiveCoinSystem MainSystem;
        
        public void OnEnter(DecisiveCoinSystem system)
        {
            MainSystem = system;
            
            DOTween.Sequence()
                .Append(MainSystem.HideCoin())
                .Join(MainSystem.HideTossResultText())
                .Append(MainSystem.HideScreenMask())
                .OnComplete(() => Debug.Log("Finish All Hide Methods."));
        }
    }
}