using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM.STATE_TYPE
{
    internal class ShowTossResult : IState
    {
        private DecisiveCoinSystem DecisiveCoinSystem;
        
        public void OnEnter(DecisiveCoinSystem system)
        {
            DecisiveCoinSystem = system;
            DecisiveCoinSystem.MoveToShowPoint(() => Debug.Log("Show Toss Result Text."));
            // TODO 從這繼續開發
        }

        public void OnExit()
        {
        }
    }
}