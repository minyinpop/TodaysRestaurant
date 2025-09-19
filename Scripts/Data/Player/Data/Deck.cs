using System;
using System.Battle_System.Object.Card.Base;
using System.Battle_System.Object.Card.Type.Battle.System.Main;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Player.Data
{
    [Serializable]
    internal sealed class Deck
    {
        [field: Header("Prefabs")]
        [field: SerializeField] private GameObject[] CardPrefabs;

        public bool GetRandomBattleCard(out GameObject cardPrefab)
        {
            var totalDrawChance = 0f;
            
            foreach (var prefab in CardPrefabs)
            {
                prefab.GetComponent<ICard>().GetDrawChance(out var drawChance);
                totalDrawChance += drawChance;
            }
            
            var randomDrawChance = Random.Range(0, totalDrawChance);
        
            foreach (var prefab in CardPrefabs)
            {
                prefab.GetComponent<ICard>().GetDrawChance(out var drawChance);
                randomDrawChance -= drawChance;
                
                if (randomDrawChance > 0) continue;

                cardPrefab = prefab;
                return true;
            }
        
            cardPrefab = null;
            return false;
        }
    }
}