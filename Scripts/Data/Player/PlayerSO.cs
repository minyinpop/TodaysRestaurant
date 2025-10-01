using System.Collections.Generic;
using Data.General;
using Data.Item.Type.Dish;
using Data.Item.Type.Ingredient;
using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(menuName = "Minyinpop/Player Data", fileName = "New Data")]
    internal sealed class PlayerSO : ScriptableObject
    {
        #region Team
        [field: Header("Team")]
        [field: SerializeField] private Team TeamData;

        public void GetCharacterNumber(out int number)
        {
            number = TeamData.CharacterNumber;
        }
        #endregion
        
        #region Deck
        [field: Header("Deck")]
        [field: SerializeField] private Deck DeckData;

        public void GetCardPrefabs(out List<GameObject> cardPrefabs)
        {
            DeckData.Get(out cardPrefabs);
        }
        #endregion
        
        #region Unlock Dishes
        [field: Header("Unlock Dishes")]
        [field: SerializeField] private List<DishSO> UnlockedDishes;

        public void GetUnlockedDishes(out List<DishSO> dishes)
        {
            dishes = UnlockedDishes;
        }
        #endregion
    }
}