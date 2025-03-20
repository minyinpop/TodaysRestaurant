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
        private bool _isFinished;

        /// <summary>
        /// 用來調度 StockpotSpoon 與 StockpotProgressBar 這兩個類之間的邏輯。
        /// 當前為玩家攪拌時，增加進度條的方法。
        /// </summary>
        public void AddProgress()
        {
            if (!_isFinished)
                progressBar.AddProgress();
        }

        /// <summary>
        /// 用於結束攪拌小遊戲的方法。
        /// </summary>
        public void Finish()
        {
            _isFinished = true;
            
            print("小遊戲結束");
            // TODO: 進度條滿了後，就完成攪拌的小遊戲。
        }
    }
}
