using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Item.Data.Custom
{
    internal sealed class CustomItem : ItemSO
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
        
        #region Name
            public override void GetItemName(out string itemName) =>
                LegacyItemData.GetItemName(out itemName);
        #endregion
        
        #region Sprite
            public override void GetItemSprite(out Sprite itemSprite) =>
                LegacyItemData.GetItemSprite(out itemSprite);
        #endregion

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
            public override void OnSelected() { }
            public override void OnAttack() { }
            public override void OnUse() { }
        #endregion
    }
}