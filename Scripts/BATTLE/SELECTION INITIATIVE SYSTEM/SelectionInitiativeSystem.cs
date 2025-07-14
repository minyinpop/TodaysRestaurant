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
        
        
        
        /// <summary>
        /// 生成決定誰先手的硬幣
        /// </summary>
        private void SpawnCoin()
        {
            Coin = Instantiate(CoinPrefab, SpawnParent);
            CoinScript = Coin.GetComponent<InitiativeCoin>();
            
            CoinScript.Finish += OnCoinRollCompleted;
            CoinScript.SetPosToReady(ReadyParent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initiativeState"></param>
        private void OnCoinRollCompleted(IState initiativeState)
        {
            // TODO 接著製作硬幣移到畫面正中間，然後顯示結果德程式碼
            switch (initiativeState)
            {
                case OnPlayerRound:
                    Debug.Log("Player First!");
                    break;
                case OnEnemyRound:
                    Debug.Log("Enemy First!");
                    break;
            }
        }
    }
}