using System.Collections.Generic;
using UnityEngine;

namespace Data.Item.Base
{
    internal abstract class ItemSO : ScriptableObject
    {
        #region Name
        public virtual void GetItemName(out string itemName)
        {
            itemName = null;
        }
        #endregion
        
        #region Sprite
        public virtual void GetItemSprite(out Sprite itemSprite)
        {
            itemSprite = null;
        }
        #endregion
        
        #region Recipe Sheet
        public virtual void GetRecipeSheet(out List<ItemSO> recipeSheet)
        {
            recipeSheet = null;
        }
        #endregion
        
        #region Cook Time
        public virtual void GetCookTime(out float cookTime)
        {
            cookTime = 0;
        }
        #endregion
        
        #region Price
        public virtual void GetPrice(out int price)
        {
            price = 0;
        }
        #endregion
    }
}