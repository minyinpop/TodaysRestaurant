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

        [Header("進度條設定"), Tooltip("進度條的移動速度，用於 UI。"), SerializeField]
        private float progressBarFillSpeed;
        
        // 進度條的滑軌組件，用於 UI。
        private Slider _progressBar;

        // 用於告訴進度條的長度要到哪裡，用於 Mathf.Lerp 使用。
        private float _targetValue;

        private void Awake()
        {
            _progressBar = GetComponent<Slider>();
        }

        private void Update()
        {
            RefreshProgress();
        }

        /// <summary>
        /// 用於增加進度條的方法。
        /// </summary>
        public void AddProgress()
        {
            _targetValue += .3f;
            _targetValue = Mathf.Clamp(_targetValue,_progressBar.minValue, _progressBar.maxValue);
            
            // 如果 _targetValue 與 _progressBar 的最大值為一致，就結束小遊戲。
            if (Mathf.Approximately(_targetValue, _progressBar.maxValue))
                stockpotManager.Finish();
        }

        /// <summary>
        /// 用於更新進度條的顯示，用於 UI。
        /// </summary>
        private void RefreshProgress()
        {
            _progressBar.value = Mathf.Lerp(_progressBar.value, _targetValue, progressBarFillSpeed * Time.deltaTime);
        }
    }
}
