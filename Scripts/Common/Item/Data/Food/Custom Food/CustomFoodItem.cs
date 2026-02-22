using Common.Value;
using UnityEngine;

namespace Common.Item.Data.Food.Custom_Food
{
    public sealed class CustomFoodItem : IFood
    {
        private IFood _food;
        private float _overrideCookTime;
        private int _overridePrice;

        public void Initialize(IFood foodData, float cookTime, int price)
        {
            if (_food is not null) return;
            _food = foodData;
            _overrideCookTime = cookTime;
            _overridePrice = price;
        }
        
        #region ID
            public int ItemID => _food.ItemID;
        #endregion
        
        #region Name
            public string ItemName => _food.ItemName;
        #endregion
        
        #region Sprite
            public Sprite ItemSprite => _food.ItemSprite;
        #endregion
        
        #region Item Type
            // public void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType) =>
            //     _food.GetItemType(out itemType, out cookType, out foodType);
            // public void GetItemType(out ItemType itemType, out int itemLevel) =>
            //     _food.GetItemType(out itemType, out itemLevel);
        #endregion

        #region Recipe Sheet
            public RecipeSheet RecipeSheet => _food.RecipeSheet;
        #endregion

        #region Cook Time
            public float CookTime => _overrideCookTime;
        #endregion

        #region Price
            public int Price => _overridePrice;
        #endregion
        
        #region Overcook
            public IItem OvercookedItem => _food.OvercookedItem;
        #endregion

        #region Interaction
            public void Selected() { }
            public void UnSelected() { }
            public void Use() { }
            public void Remove() { }
        #endregion
    }
}