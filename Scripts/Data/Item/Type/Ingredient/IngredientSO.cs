using Data.General.Enum.Item.Main;
using Data.Item.Base;
using UnityEngine;

namespace Data.Item.Type.Ingredient
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient Data", fileName = "New Data")]
    internal sealed class IngredientSO : ItemSO
    {
        #region Name
            [field: Header("Name")]
            [field: SerializeField] private string ItemName;
            public override void GetItemName(out string itemName)
            {
                itemName = ItemName;
            }
        #endregion
        
        #region Sprite
            [field: Header("Sprite")]
            [field: SerializeField] private Sprite ItemSprite;
            public override void GetItemSprite(out Sprite itemSprite)
            {
                itemSprite = ItemSprite;
            }
        #endregion

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
    }
}