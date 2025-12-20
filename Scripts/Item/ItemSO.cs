using System;
using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Item
{
    public abstract class ItemSO : ScriptableObject
    {
        #region Name
            [field: Header("Item Name")]
            [field: SerializeField] private string itemName;
            public string ItemName => itemName;
        #endregion
        
        #region Sprite
            [field: Header("Item Sprite")]
            [field: SerializeField] private Sprite itemSprite;
            public Sprite ItemSprite => itemSprite;
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
            public abstract void Selected();
            public abstract void UnSelected();
            public abstract void Use();
        #endregion
    }
}