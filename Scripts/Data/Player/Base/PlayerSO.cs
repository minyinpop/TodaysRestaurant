using System.Collections.Generic;
using Data.Food.Food_Category.Base;
using Data.General;
using UnityEngine;

namespace Data.Player.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Player Data", fileName = "New Data")]
    internal sealed class PlayerSO : ScriptableObject
    {
        #region Team
            [field: Header("Team")]
            [field: SerializeField] private Team TeamData;
            public void GetCharacterNumber(out int number) { number = TeamData.CharacterNumber; }
        #endregion
        
        #region Deck
            [field: Header("Deck")]
            [field: SerializeField] private Deck DeckData;
            public void GetCardPrefabs(out List<GameObject> cardPrefabs) { DeckData.Get(out cardPrefabs); }
        #endregion
        
        #region Unlock Foods
            [field: Header("Unlock Foods")]
            [field: SerializeField] private List<FoodCategorySO> UnlockFoods;
            public void GetUnlockFoods(out List<FoodCategorySO> foods) { foods = UnlockFoods; }
        #endregion
    }
}