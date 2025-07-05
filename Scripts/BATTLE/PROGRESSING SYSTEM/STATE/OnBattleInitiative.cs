using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.PROGRESSING_SYSTEM.STATE
{
    /// <summary>
    /// 控制戰鬥進程的系統在開場狀態執行完畢後，就會自動來到投擲硬幣，決定玩家或是敵人哪一方先行動的狀態
    /// </summary>
    internal class OnBattleInitiative : IState
    {
        // TODO 當玩家投擲完硬幣後，就會自動觸發一個 Broadcast，讓管理戰鬥進程的系統知道
        
        public void Enter()
        {
            DOTween.Sequence()
                .AppendInterval(1)
                .OnComplete(() =>
                {
                    Debug.Log("玩家投擲硬幣結束");
                    // TODO Invoke Broadcast Here...
                });
        }

        public void Exit()
        {
            
        }
    }
}