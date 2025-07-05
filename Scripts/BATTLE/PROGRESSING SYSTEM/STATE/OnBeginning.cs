using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.PROGRESSING_SYSTEM.STATE
{
    /// <summary>
    /// 當玩家開始戰鬥時，控制戰鬥進程的系統，首先會來到這裡
    /// </summary>
    internal class OnBeginning : IState
    {
        /// <summary>
        /// 當 Enter() 裡執行結束後，就會自動發出該 Broadcast，以讓管理戰鬥進程的系統，可以進到下個狀態
        /// </summary>
        public event System.Action Finish;
        
        public void Enter()
        {
            DOTween.Sequence()
                .AppendInterval(1)
                .OnComplete(() =>
                {
                    Debug.Log("相機對焦敵人結束");
                    Finish?.Invoke();
                });
        }

        public void Exit()
        {
            Debug.Log("觸發 OnBeginning 的 Exit");
        }
    }
}