using Common.Value;
using UnityEngine;

namespace Common.Item.Data.Ingredient
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient", fileName = "New Data")]
    public sealed class IngredientSO : ItemSO, IIngredient
    {
        [field: Header("Information")]
        [field: SerializeField] private IngredientTier ingredientTier;
                                public IngredientTier IngredientTier => ingredientTier;
        [field: SerializeField] private float cookTime;
                                public float CookTime => cookTime;
        [field: SerializeField] private int price;
                                public int Price => price;
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
            public override void Remove() { }
        #endregion
    }
}