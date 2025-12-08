using System.Collections.Generic;
using Data.General.Enum;
using Data.Item.Interface;
using UnityEngine;

namespace Data.Item.Data.Food
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Dish Data", fileName = "New Data")]
    internal sealed class FoodSO : ScriptableObject, ITem
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
            [field: SerializeField] private CookType CookType;
            [field: SerializeField] private FoodType FoodType;
            public void GetItemType(out ItemType itemType, out int itemLevel)
            {
                // TODO
                throw new System.NotImplementedException();
            }
            
            public void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType)
            {
                itemType = ItemType;
                cookType = CookType;
                foodType = FoodType;
            }
        #endregion
        
        #region Recipe Sheet
            [field: Header("Recipe Sheet")]
            [field: SerializeField] private List<ITem> RecipeSheet;
            public void GetRecipeSheet(out List<ITem> recipeSheet)
            {
                recipeSheet = RecipeSheet;
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