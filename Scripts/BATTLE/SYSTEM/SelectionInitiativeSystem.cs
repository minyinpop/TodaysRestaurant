using UnityEngine;

namespace BATTLE.SYSTEM
{
    /// <summary>
    /// 戰鬥進程的子系統
    /// 專門用來管理決定哪方先行動的程式碼
    /// </summary>
    internal class SelectionInitiativeSystem : MonoBehaviour
    {
        /// <summary>
        /// 用來決定哪方先行動的硬幣的預製件
        /// </summary>
        [field: SerializeField] private GameObject CoinPrefab;
        
        /// <summary>
        /// 硬幣的生成點
        /// </summary>
        [field: SerializeField] private RectTransform SpawnPos;
        /// <summary>
        /// 硬幣準備被投擲的位置點
        /// </summary>
        [field: SerializeField] private RectTransform TossPos;
        
        /// <summary>
        /// 用來暫存硬幣
        /// </summary>
        private GameObject Coin;
        /// <summary>
        /// 用來暫存硬幣的主程式碼
        /// </summary>
        private SelectionInitiativeCoin CoinScript;

        /// <summary>
        /// 將硬幣創建在生成點上，並設定座標與其點為一致
        /// </summary>
        public void SpawnCoin()
        {
            var spawnPosX = SpawnPos.anchoredPosition.x;
            var spawnPosY = SpawnPos.anchoredPosition.y;
            
            Coin = Instantiate(CoinPrefab, new Vector2(spawnPosX, spawnPosY), Quaternion.identity, SpawnPos);
            CoinScript = Coin.GetComponent<SelectionInitiativeCoin>();
        }
    }
}