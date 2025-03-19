using UnityEngine;
using UnityEngine.UI;

namespace Game.Stockpot
{
    /// <summary>
    /// 這是關於深煮鍋的烹飪小遊戲的進度條的類，專門告訴玩家距離攪拌完成還有多長。
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class StockpotProgressBar : MonoBehaviour
    {
        [Header("組件"), Tooltip("深煮鍋小遊戲的管理器的類。"), SerializeField]
        private StockpotManager stockpotManager;
        
        // 進度條的滑軌組件，用於 UI。
        private Slider _progressBar;

        private void Awake()
        {
            _progressBar = GetComponent<Slider>();
        }
    }
}
