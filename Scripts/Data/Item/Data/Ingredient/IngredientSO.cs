using System.Collections.Generic;
using Data.General.Enum;
using Data.Item.Interface;
using UnityEngine;

namespace Data.Item.Data.Ingredient
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient Data", fileName = "New Data")]
    internal sealed class IngredientSO : ScriptableObject, ITem
    {
        #region Name
            [field: Header("Name")]
            [field: SerializeField] private string ItemName;
            public void GetItemName(out string itemName)
            {
                itemName = ItemName;
            }
        #endregion
        
        #region Sprite
            [field: Header("Sprite")]
            [field: SerializeField] private Sprite ItemSprite;
            public void GetItemSprite(out Sprite itemSprite)
            {
                itemSprite = ItemSprite;
            }
        #endregion

        #region Item Type
            [field: Header("Item Type")]
            [field: SerializeField] private ItemType ItemType;
            [field: SerializeField, Range(1, 3)] private int ItemLevel;
            public void GetItemType(out ItemType itemType, out int itemLevel)
            {
                itemType = ItemType;
                itemLevel = ItemLevel;
            }
            
            public void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType)
            {
                // TODO
                throw new System.NotImplementedException();
            }
        #endregion
        
        #region Recipe Sheet 
            public void GetRecipeSheet(out List<ITem> recipeSheet)
            {
                // TODO
                throw new System.NotImplementedException();
            }
        #endregion

        #region Cook Time
            [field: Header("Cook Time")]
            [field: SerializeField] private float CookTime;
            public void GetCookTime(out float cookTime)
            {
                cookTime = Mathf.Abs(CookTime);
            }
        #endregion
        
        #region Price
            [field: Header("Price")]
            [field: SerializeField] private int Price;
            public void GetPrice(out int price)
            {
                price = Mathf.Abs(Price);
            }
        #endregion
    }
}