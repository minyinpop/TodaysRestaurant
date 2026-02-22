using Common.Item.Object;
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
        [field: SerializeField] private ItemObject itemObject;
                                public ItemObject ItemObject => itemObject;

        private void OnValidate()
        {
            if (cookTime < 0)
            {
                Debug.Log($"{ItemName} > {GetType().Name} > {nameof(cookTime)} cannot be negative.");
                return;
            }

            if (price < 0)
            {
                Debug.Log($"{ItemName} > {GetType().Name} > {nameof(price)} cannot be negative.");
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