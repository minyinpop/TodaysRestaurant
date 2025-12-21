using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Item.Food
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Dish Data", fileName = "New Data")]
    public sealed class FoodSO : ItemSO
    {
        #region Item Type
            [field: Header("Item Type")]
            [field: SerializeField] private ItemType ItemType;
            [field: SerializeField] private CookType CookType;
            [field: SerializeField] private FoodType FoodType;
            public override void GetItemType(out ItemType itemType, out int itemLevel)
            {
                // TODO
                throw new System.NotImplementedException();
            }
            
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
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
    }
}