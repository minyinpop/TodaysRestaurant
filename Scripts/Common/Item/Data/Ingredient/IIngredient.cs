using Common.Item.Object;
using Common.Value;
using Common.Value.Type;

namespace Common.Item.Data.Ingredient
{
    public interface IIngredient : IItem
    {
        public IngredientType IngredientType { get; }
        
        public IngredientTier IngredientTier { get; }
        
        public float CookTime { get; }
        
        public int Price { get; }
        
        public ItemObject ItemObject { get; }
    }
}