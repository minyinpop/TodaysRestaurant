using System.Collections.Generic;
using Common.Item.Data.Ingredient;
using Common.Value;

namespace Common.Item.Data.Food
{
    public interface IFood : IItem
    {
        public RecipeSheet RecipeSheet { get; }
        
        public float CookTime { get; }
        
        public int Price { get; }
        
        public IItem OvercookedItem { get; }
    }
}