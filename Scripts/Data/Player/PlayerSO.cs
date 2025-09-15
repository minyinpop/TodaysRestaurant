using Data.Character.Player.Data;
using Data.Player.Data;
using UnityEngine;

namespace Data.Character.Player
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Data", fileName = "New Data")]
    internal sealed class PlayerSO : ScriptableObject
    {
        [field: SerializeField] private Team TeamData;
        [field: SerializeField] private Deck DeckData;

        public void GetCharacterNumber(out int number)
        {
            number = TeamData.CharacterNumber;
        }
        
        public bool GetRandomBattleCard(out GameObject cardPrefab)
        {
            return DeckData.GetRandomBattleCard(out cardPrefab);
        }
    }
}