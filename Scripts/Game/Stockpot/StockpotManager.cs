using System.Collections;
using Kitchenware;
using UnityEngine;

namespace Game.Stockpot
{
    public class StockpotManager : MonoBehaviour
    {
        [Header("組件"), Tooltip("深煮鍋的湯匙的類，用於管理與串聯數據用。"), SerializeField]
        private StockpotSpoon spoon;
        
        [Tooltip("深煮鍋的攪拌進度條的類，用於管理與串聯數據用。"), SerializeField]
        private StockpotProgressBar progressBar;
        
        // 用於判斷小遊戲是否結束，即為攪拌結束。
        public bool IsFinished { get; private set; }

        // 用於結束遊戲的異步協程。
        private IEnumerator _finishProcess;
        
        // 目標廚俱的 KitchenwareManager 組件，
        private KitchenwareManager _kitchenwareManager;
        
        private void OnDisable()
        {
            if (_finishProcess is not null)
            {
                StopCoroutine(_finishProcess);
                _finishProcess = null;
            }
        }

        /// <summary>
        /// 用來初始化所執行的方法。
        /// </summary>
        /// <param name="kitchenwareManager"> 目標廚俱的管理器的類。 </param>
        public void OnInit(KitchenwareManager kitchenwareManager)
        {
            _kitchenwareManager = kitchenwareManager;
        }

        /// <summary>
        /// 用來調度 StockpotSpoon 與 StockpotProgressBar 這兩個類之間的邏輯。
        /// 當前為玩家攪拌時，增加進度條的方法。
        /// </summary>
        public void AddProgress()
        {
            if (!IsFinished)
                progressBar.AddProgress();
        }

        /// <summary>
        /// 用於結束攪拌小遊戲的方法。
        /// </summary>
        public void Finish()
        {
            _finishProcess = FinishProcess();
            StartCoroutine(_finishProcess);
        }

        private IEnumerator FinishProcess()
        {
            IsFinished = true;
            spoon.FinishGame();
            
            yield return new WaitForSeconds(1);
            _kitchenwareManager.OnGameFinish();
            
            // TODO: 未來可以做一些結束時的動畫 ......
        }
    }
}
