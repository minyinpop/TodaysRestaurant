using System.Collections.Generic;
using Common.Value.Type;

namespace Item.Custom
{
    public sealed class CustomItem : ItemSO
    {
        private readonly ItemSO LegacyItemData;
        private readonly float OverrideCookTime;
        private readonly int OverridePrice;

        public CustomItem(ItemSO itemData, float cookTime, int price)
        {
            LegacyItemData = itemData;
            OverrideCookTime = cookTime;
            OverridePrice = price;
        }
        
        #region Item Type
            public override void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType) =>
                LegacyItemData.GetItemType(out itemType, out cookType, out foodType);
            public override void GetItemType(out ItemType itemType, out int itemLevel) =>
                LegacyItemData.GetItemType(out itemType, out itemLevel);
        #endregion

        #region Recipe Sheet
            public override void GetRecipeSheet(out List<ItemSO> recipeSheet) =>
                LegacyItemData.GetRecipeSheet(out recipeSheet);
        #endregion

        #region Cook Time
            public override void GetCookTime(out float cookTime) =>
                cookTime = OverrideCookTime;
        #endregion

        #region Price
        public override void GetPrice(out int price) =>
            price = OverridePrice;
        #endregion

        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
    }
}