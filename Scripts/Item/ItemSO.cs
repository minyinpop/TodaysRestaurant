using System;
using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Item
{
    public abstract class ItemSO : ScriptableObject, ITem
    {
        #region Name
            public virtual void GetItemName(out string itemName) =>
                throw new NotImplementedException();
        #endregion
        
        #region Sprite
            public virtual void GetItemSprite(out Sprite itemSprite) =>
                throw new NotImplementedException();
        #endregion
        
        #region Item Type
            public virtual void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType) =>
                throw new NotImplementedException();
            public virtual void GetItemType(out ItemType itemType, out int itemLevel) =>
                throw new NotImplementedException();
        #endregion
        
        #region Recipe Sheet
            public virtual void GetRecipeSheet(out List<ItemSO> recipeSheet) =>
                throw new NotImplementedException();
        #endregion
        
        #region Cook Time
            public virtual void GetCookTime(out float cookTime) =>
                throw new NotImplementedException();
        #endregion
        
        #region Price
            public virtual void GetPrice(out int price) =>
                throw new NotImplementedException();
        #endregion
        
        #region Interaction
            public abstract void OnSelected();
            public abstract void OnAttack();
            public abstract void OnUse();
        #endregion
    }
}