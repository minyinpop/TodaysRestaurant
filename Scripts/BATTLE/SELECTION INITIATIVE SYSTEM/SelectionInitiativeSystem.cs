using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.PROGRESSING_SYSTEM.STATE;
using UnityEngine;
using UTILITY;

namespace BATTLE.SELECTION_INITIATIVE_SYSTEM
{
    internal class SelectionInitiativeSystem : MonoBehaviour
    {
        /// <summary>
        /// 硬幣生成後的父物件
        /// </summary>
        [field: SerializeField] private RectTransform SpawnParent;
        /// <summary>
        /// 用於決定哪一方先手的硬幣
        /// </summary>
        [field: SerializeField] private GameObject CoinPrefab;
        private GameObject Coin;
        private InitiativeCoin CoinScript;
        
        /// <summary>
        /// 等待玩家投擲的預備位置乘數
        /// </summary>
        [field: SerializeField] private AnchorsMult ReadyPosMult;
        /// <summary>
        /// 顯示硬幣投擲結果的位置乘數
        /// </summary>
        [field: SerializeField] private AnchorsMult ShowParentMult;
        
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
            CoinScript.SetPosToReady(ReadyPosMult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initiativeState"></param>
        private void OnCoinRollCompleted(IState initiativeState)
        {
            // CoinScript.SetPosToShow(ShowParent);
            
            switch (initiativeState)
            {
                case OnPlayerRound:
                {
                    break;
                }
                case OnEnemyRound:
                {
                    break;
                }
            }
        }
    }
}