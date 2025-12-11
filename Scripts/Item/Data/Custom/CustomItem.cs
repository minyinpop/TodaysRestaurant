using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Item.Data.Custom
{
    internal sealed class CustomItem : ITem
    {
        private readonly ITem LegacyItemData;
        private readonly float OverrideCookTime;
        private readonly int OverridePrice;

        public CustomItem(ITem itemData, float cookTime, int price)
        {
            LegacyItemData = itemData;
            OverrideCookTime = cookTime;
            OverridePrice = price;
        }

        #region ITem
            public void GetItemName(out string itemName) =>
                LegacyItemData.GetItemName(out itemName);
            
            public void GetItemSprite(out Sprite itemSprite) =>
                LegacyItemData.GetItemSprite(out itemSprite);

            public void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType) =>
                LegacyItemData.GetItemType(out itemType, out cookType, out foodType);

            public void GetItemType(out ItemType itemType, out int itemLevel) =>
                LegacyItemData.GetItemType(out itemType, out itemLevel);

            public void GetRecipeSheet(out List<ItemSO> recipeSheet) =>
                LegacyItemData.GetRecipeSheet(out recipeSheet);

            public void GetCookTime(out float cookTime) =>
                cookTime = OverrideCookTime;

            public void GetPrice(out int price) =>
                price = OverridePrice;
        #endregion
    }
}