using Common.Item.Object;
using Common.Value;

namespace Common.Item.Data.Ingredient
{
    public interface IIngredient : IItem
    {
        public IngredientTier IngredientTier { get; }
        
        public float CookTime { get; }
        
        public int Price { get; }
        
        public ItemObject ItemObject { get; }
    }
}