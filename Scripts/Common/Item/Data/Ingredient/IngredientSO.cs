using Common.Item.Object;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Common.Item.Data.Ingredient
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient", fileName = "New Data")]
    public sealed class IngredientSO : ItemSO, IIngredient
    {
        [field: Header("子資料 - 食材類型")]
        [field: SerializeField] private IngredientType ingredientType;
                                public IngredientType IngredientType => ingredientType;
                                
        [field: Header("子資料 - 食材等級")]
        [field: SerializeField] private IngredientTier ingredientTier;
                                public IngredientTier IngredientTier => ingredientTier;
                                
        [field: Header("子資料 - 烹飪時間")]
        [field: SerializeField] private float cookTime;
                                public float CookTime => cookTime;
                                
        [field: Header("子資料 - 食材價格")]
        [field: SerializeField] private int price;
                                public int Price => price;
                                
        [field: Header("子資料 - 食材預製件")]
        [field: SerializeField] private ItemObject itemObject;
                                public ItemObject ItemObject => itemObject;
        
        private void OnValidate()
        {
            if (cookTime < 0)
            {
                Debug.Log($"{name} 的 {nameof(cookTime)} 參數不能為負數。");
            }

            if (price < 0)
            {
                Debug.Log($"{name} 的 {nameof(price)} 參數不能為負數。");
            }
        }

        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
            public override void Remove() { }
        #endregion
    }
}