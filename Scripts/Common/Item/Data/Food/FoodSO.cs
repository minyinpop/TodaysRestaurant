using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Common.Item.Data.Food
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Food", fileName = "New Data")]
    public sealed class FoodSO : ItemSO, IFood
    {
        #region Item Type
            [field: Header("Item Type")]
            [field: SerializeField] private ItemType ItemType;
            [field: SerializeField] private CookType CookType;
            [field: SerializeField] private FoodType FoodType;
            public void GetItemType(out ItemType itemType, out int itemLevel)
            {
                // TODO
                throw new System.NotImplementedException();
            }
            
            public override  void GetItemType(out ItemType itemType, out CookType cookType, out FoodType foodType)
            {
                itemType = ItemType;
                cookType = CookType;
                foodType = FoodType;
            }
        #endregion
        
        #region Recipe Sheet
            [field: Header("Recipe Sheet")]
            [field: SerializeField] private RecipeSheet recipeSheet;
                                    public RecipeSheet RecipeSheet => recipeSheet;
        #endregion
        
        #region Cook Time
            [field: Header("Cook Time")]
            [field: SerializeField] private float cookTime;
                                    public float CookTime
                                    {
                                        get
                                        {
                                            if (cookTime < 0)
                                            {
                                                Debug.Log($"{ItemName} > {GetType().Name} > {nameof(cookTime)} cannot be negative.");
                                                return 0;
                                            }

                                            return cookTime;
                                        }
                                    }
        #endregion
        
        #region Price
            [field: Header("Price")]
            [field: SerializeField] private int price;
                                    public int Price => price;
        #endregion
        
        #region Overcook
            [field: Header("Overcook")]
            [field: SerializeField] private FoodSO overcookedItem;
                                    public IItem OvercookedItem
                                    {
                                        get
                                        {
                                            if (overcookedItem == null)
                                            {
                                                Debug.Log($"{ItemName} > {GetType().Name} > {nameof(overcookedItem)} cannot be null.");
                                                return null;
                                            }
                                            
                                            return OvercookedItem;
                                        }
                                    }
        #endregion
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
            public override void Remove() { }
        #endregion
    }
}