using Common.Value.Type;
using UnityEngine;

namespace Item.Data.Ingredient
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient Data", fileName = "New Data")]
    internal sealed class IngredientSO : ItemSO
    {
        #region Item Type
            [field: Header("Item Type")]
            [field: SerializeField] private ItemType ItemType;
            [field: SerializeField, Range(1, 3)] private int ItemLevel;
            public override void GetItemType(out ItemType itemType, out int itemLevel)
            {
                itemType = ItemType;
                itemLevel = ItemLevel;
            }
        #endregion

        #region Cook Time
            [field: Header("Cook Time")]
            [field: SerializeField] private float CookTime;
            public override void GetCookTime(out float cookTime)
            {
                cookTime = Mathf.Abs(CookTime);
            }
        #endregion
        
        #region Price
            [field: Header("Price")]
            [field: SerializeField] private int Price;
            public override void GetPrice(out int price)
            {
                price = Mathf.Abs(Price);
            }
        #endregion
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
    }
}