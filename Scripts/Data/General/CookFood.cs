using Data.Item.Type.Food;

namespace Data.General
{
    internal sealed class CookFood
    {
        private readonly FoodSO FoodData;
        private readonly float CookTime;
        private readonly int Price;

        public CookFood(FoodSO foodData, float cookTime, int price)
        {
            FoodData = foodData;
            CookTime = cookTime;
            Price = price;
        }

        public void GetValues(out FoodSO foodData, out float cookTime, out int price)
        {
            foodData = FoodData;
            cookTime = CookTime;
            price = Price;
        }
    }
}