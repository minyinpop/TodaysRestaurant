using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Item
{
    public interface ITem
    {
        #region Name
            public void GetItemName(out string itemName);
        #endregion
        
        #region Sprite
            public void GetItemSprite(out Sprite itemSprite);
        #endregion
        
        #region Item Type
            public void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType);
            public void GetItemType(out ItemType itemType, out int itemLevel);
        #endregion
        
        #region Recipe Sheet
            public void GetRecipeSheet(out List<ItemSO> recipeSheet);
        #endregion
        
        #region Cook Time
            public void GetCookTime(out float cookTime);
        #endregion
        
        #region Price
            public void GetPrice(out int price);
        #endregion
    }
}