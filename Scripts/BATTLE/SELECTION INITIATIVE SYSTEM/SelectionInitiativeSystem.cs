using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.PROGRESSING_SYSTEM.STATE;
using UnityEngine;

namespace BATTLE.SELECTION_INITIATIVE_SYSTEM
{
    internal class SelectionInitiativeSystem : MonoBehaviour
    {
        /// <summary>
        /// 硬幣的生成位置
        /// </summary>
        [field: SerializeField] private RectTransform SpawnParent;
        /// <summary>
        /// 等待玩家投擲的預備位置
        /// </summary>
        [field: SerializeField] private RectTransform ReadyParent;
        /// <summary>
        /// 顯示硬幣投擲結果的位置
        /// </summary>
        [field: SerializeField] private RectTransform ShowParent;
        /// <summary>
        /// 用於決定哪一方先手的硬幣
        /// </summary>
        [field: SerializeField] private GameObject CoinPrefab;
        private GameObject Coin;
        private InitiativeCoin CoinScript;

        private void OnEnable()
        {
            OnBattleInitiative.OnEnter += SpawnCoin;
        }
        
        private void OnDisable()
        {
            OnBattleInitiative.OnEnter -= SpawnCoin;
        }
        
        
        
        private void SpawnCoin()
        {
            Coin = Instantiate(CoinPrefab, SpawnParent);
            CoinScript = Coin.GetComponent<InitiativeCoin>();
            CoinScript.SetPosToReady(ReadyParent);
        }
    }
}