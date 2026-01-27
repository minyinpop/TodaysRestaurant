using System.Collections.Generic;
using Common.Item.Food.Data.Food_Category;
using Player_System.Data.Child.Attribute;
using Player_System.Data.Child.Deck;
using Player_System.Data.Child.Inventory;
using Player_System.Data.Child.Team;
using Player_System.Data.Child.Unlock_Food;
using UnityEngine;

namespace Player_System.Data.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Main/Player Data", fileName = "New Data")]
    internal sealed class PlayerSO : ScriptableObject
    {
        #region Team
            [field: Header("Team")]
            [field: SerializeField] private TeamSO TeamData;
            public void GetCharacterNumber(out int number) => TeamData.GetCharacterNumber(out number);
        #endregion
        
        #region Deck
            [field: Header("Deck")]
            [field: SerializeField] private DeckSO DeckData;
            public void GetCardPrefabs(out List<GameObject> cardPrefabs) => DeckData.Get(out cardPrefabs);
        #endregion
        
        #region Unlock Food
            [field: Header("Unlock Food")]
            [field: SerializeField] private UnlockFoodSO UnlockFoodData;
            public void GetUnlockFoods(out FoodCategorySO[] foods) => UnlockFoodData.GetUnlockFoods(out foods);
        #endregion

        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private AttributeSO AttributeData;
            public void GetMoveSpeed(out float moveSpeed) => AttributeData.GetMoveSpeed(out moveSpeed);
        #endregion
        
        #region Inventory
            [field: Header("Inventory")]
            [field: SerializeField] private InventorySO InventoryData;
        #endregion
    }
}