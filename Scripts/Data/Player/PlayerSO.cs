using System.Battle_System.Object.Card.Type.Battle.System.Main;
using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Battle Deck", fileName = "New Data")]
    internal sealed class PlayerSO : ScriptableObject
    {
        [field: Header("State")]
        [field: SerializeField] private int MaxHealth;
        [field: SerializeField] private int CurrentHealth;
        
        [field: Header("Character")]
        [field: SerializeField] private int ActiveCharacterNumber;

        public void GetActiveCharacterNumber(out int number)
        {
            number = ActiveCharacterNumber;
        }

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