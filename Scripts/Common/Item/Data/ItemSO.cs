using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Common.Item.Data
{
    public abstract class ItemSO : ScriptableObject, IItem
    {
        [field: Header("Information")]
        [field: SerializeField] private int itemID;
                                public int ItemID => itemID;
        [field: SerializeField] private string itemName;
                                public string ItemName => itemName;
        [field: SerializeField] private Sprite itemSprite;
                                public Sprite ItemSprite => itemSprite;
                                
        // [field: Header("")]
        // [field: SerializeField] private Object.Item itemPrefab;
        //                         public Object.Item ItemPrefab => itemPrefab;
        
        #region Item Type
            public virtual void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType)
            {
                itemType = ItemType.Null;
                cookType = CookType.Null;
                foodType = FoodType.Null;
            }
        
            public virtual void GetItemType(out ItemType itemType, out int itemLevel)
            {
                itemType = ItemType.Null;
                itemLevel = 0;
            }
        #endregion
        
        #region Recipe Sheet
            public virtual void GetRecipeSheet(out List<ItemSO> recipeSheet)
            {
                recipeSheet = new List<ItemSO>();
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
        
        // #region Overcook
        //     public virtual void GetOvercookedItem(out ItemSO overcookedItem)
        //     {
        //         overcookedItem = null;
        //     }
        // #endregion
        
        #region Interaction
            public abstract void Selected();
            public abstract void UnSelected();
            public abstract void Use();
            public abstract void Remove();
        #endregion
    }
}