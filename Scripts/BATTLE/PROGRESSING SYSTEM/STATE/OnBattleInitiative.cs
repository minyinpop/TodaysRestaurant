using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using UnityEngine;

namespace BATTLE.PROGRESSING_SYSTEM.STATE
{
    /// <summary>
    /// 控制戰鬥進程的系統在開場狀態執行完畢後，就會自動來到投擲硬幣，決定玩家或是敵人哪一方先行動的狀態
    /// </summary>
    internal class OnBattleInitiative : IState
    {
        /// <summary>
        /// 用來發送生成硬幣的 Broadcast，讓負責管理決定誰先手的系統，來執行相對應的程序
        /// </summary>
        public static event System.Action OnEnter;
        /// <summary>
        /// 當玩家投擲完硬幣，並且展示了擲完硬幣後的結果，就會觸發這個 Broadcast，以讓流程系統知道可以到下個狀態
        /// </summary>
        public event System.Action OnFinish;
        
        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            Debug.Log("觸發 OnBattleInitiative 的 Exit");
        }
    }
}