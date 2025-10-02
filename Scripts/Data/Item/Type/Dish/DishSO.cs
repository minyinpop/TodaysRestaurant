using System.Collections.Generic;
using Data.Item.Base;
using UnityEngine;

namespace Data.Item.Type.Dish
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Dish Data", fileName = "New Data")]
    internal sealed class DishSO : ItemSO
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
            var totalTime = 0f;
            foreach (var item in RecipeSheet)
            {
                item.GetCookTime(out var time);
                totalTime += time;
            }

            totalTime += CookTime;
            cookTime = totalTime;
        }
        #endregion
        
        #region Price
        [field: Header("Price")]
        [field: SerializeField] private int Price;
        public override void GetPrice(out int price)
        {
            var totalPrice = 0;
            foreach (var item in RecipeSheet)
            {
                item.GetPrice(out var priceItem);
                totalPrice += priceItem;
            }

            totalPrice += Price;
            price = totalPrice;
        }
        #endregion
    }
}