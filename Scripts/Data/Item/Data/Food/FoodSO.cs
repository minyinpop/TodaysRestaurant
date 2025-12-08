using System.Collections.Generic;
using Data.General.Enum;
using Data.Item.Abstract;
using UnityEngine;

namespace Data.Item.Data.Food
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Dish Data", fileName = "New Data")]
    internal sealed class FoodSO : ItemSO
    {
        #region Name
            [field: Header("Name")]
            [field: SerializeField] private string ItemName;
            public override void GetItemName(out string itemName)
            {
                itemName = ItemName;
            }
        #endregion
        
        #region Sprite
            [field: Header("Sprite")]
            [field: SerializeField] private Sprite ItemSprite;
            public override void GetItemSprite(out Sprite itemSprite)
            {
                itemSprite = ItemSprite;
            }
        #endregion
        
        #region Item Type
            [field: Header("Item Type")]
            [field: SerializeField] private ItemType ItemType;
            [field: SerializeField] private CookType CookType;
            [field: SerializeField] private FoodType FoodType;
            public override void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType)
            {
                itemType = ItemType;
                cookType = CookType;
                foodType = FoodType;
            }
        #endregion
        
        #region Recipe Sheet
            [field: Header("Recipe Sheet")]
            [field: SerializeField] private List<ItemSO> RecipeSheet;
            public override void GetRecipeSheet(out List<ItemSO> recipeSheet)
            {
                recipeSheet = RecipeSheet;
            }
        #endregion
        
        #region Cook Time
            [field: Header("Cook Time")]
            [field: SerializeField] private float CookTime;
            public override void GetCookTime(out float cookTime)
            {
                cookTime = Mathf.Abs(CookTime);
            }
        #endregion
        
        #region Price
            [field: Header("Price")]
            [field: SerializeField] private int Price;
            public override void GetPrice(out int price)
            {
                price = Mathf.Abs(Price);
            }
        #endregion
    }
}