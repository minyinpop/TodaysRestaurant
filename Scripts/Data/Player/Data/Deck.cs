using System;
using System.Battle_System.Object.Card.Type.Battle.System.Main;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Player.Data
{
    [Serializable]
    internal sealed class Deck
    {
        [field: Header("Battle Card")]
        [field: SerializeField] private BattleCard[] BattleCards;

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