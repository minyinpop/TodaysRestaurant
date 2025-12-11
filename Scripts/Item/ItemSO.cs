using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Item
{
    public abstract class ItemSO : ScriptableObject, ITem
    {
        #region Name
            public virtual void GetItemName(out string itemName) =>
                throw new System.NotImplementedException($"{name}'s name is not set.");
        #endregion
        
        #region Sprite
            public virtual void GetItemSprite(out Sprite itemSprite) =>
                throw new System.NotImplementedException($"{name}'s sprite is not set.");
        #endregion
        
        #region Item Type
            public virtual void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType) =>
                throw new System.NotImplementedException($"{name}'s item type is not set.");
            public virtual void GetItemType(out ItemType itemType, out int itemLevel) =>
                throw new System.NotImplementedException($"{name}'s item type is not set.");
        #endregion
        
        #region Recipe Sheet
            public virtual void GetRecipeSheet(out List<ItemSO> recipeSheet) =>
                throw new System.NotImplementedException($"{name}'s recipe sheet is not set.");
        #endregion
        
        #region Cook Time
            public virtual void GetCookTime(out float cookTime) =>
                throw new System.NotImplementedException($"{name}'s cook time is not set.");
        #endregion
        
        #region Price
            public virtual void GetPrice(out int price) =>
                throw new System.NotImplementedException($"{name}'s price is not set.");
        #endregion
    }
}