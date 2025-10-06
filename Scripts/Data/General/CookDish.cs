using Data.Item.Type.Dish;

namespace Data.General
{
    internal sealed class CookDish
    {
        private readonly DishSO DishData;
        private readonly float CookTime;
        private readonly int Price;

        public CookDish(DishSO dishData, float cookTime, int price)
        {
            DishData = dishData;
            CookTime = cookTime;
            Price = price;
        }

        public void GetValues(out DishSO dishData, out float cookTime, out int price)
        {
            dishData = DishData;
            cookTime = CookTime;
            price = Price;
        }
    }
}