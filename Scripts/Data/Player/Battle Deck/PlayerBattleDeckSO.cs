using Card_Battle_System.Object.Card.Type.Battle.System.Main;
using UnityEngine;

namespace Data.Player.Battle_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Battle Deck", fileName = "New Data")]
    internal sealed class PlayerBattleDeckSO : ScriptableObject
    {
        [field: Header("Battle Card")]
        [field: SerializeField] private BattleCard[] BattleCards;

        /// <summary>
        /// 從戰鬥卡包中隨機抽取一張戰鬥卡片，並回傳戰鬥卡片的 Prefab 與是否獲取成功
        /// </summary>
        /// <paramref name="cardPrefab">戰鬥卡片的 Prefab</paramref>
        /// <returns>是否獲取成功</returns>
        public bool GetRandomBattleCard(out GameObject cardPrefab)
        {
            var totalDrawChance = 0f;
            
            foreach (var battleCard in BattleCards)
            {
                battleCard.GetDrawChance(out var drawChance);
                totalDrawChance += drawChance;
            }
            
            var randomDrawChance = Random.Range(0, totalDrawChance);
        
            foreach (var battleCard in BattleCards)
            {
                battleCard.GetDrawChance(out var drawChance);
                randomDrawChance -= drawChance;
                
                if (randomDrawChance > 0) continue;
                
                cardPrefab = battleCard.gameObject;
                return true;
            }
        
            cardPrefab = null;
            return false;
        }
    }
}