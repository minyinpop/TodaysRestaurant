using System;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Database.Restaurant.Menu
{
    [Serializable]
    internal class TodayDishSlot
    {
        [field: SerializeField] public bool Lock { get; private set; }
        [field: SerializeField] public DishSO Dish { get; private set; }
        [field: SerializeField] public int Portion { get; private set; }

        public void AddDish(DishSO newDish)
        {
            Lock = false;
            Dish = newDish;
            Portion = Dish.Portion;
        }

        public DishSO TakeDish()
        {
            if (Lock || Dish is null || Portion <= 0)
                return null;
            
            Portion -= 1;

            var dish = Dish;

            if (Portion <= 0)
                ClearData();
            
            return dish;
        }

        public void ClearData()
        {
            Dish = null;
            Portion = 0;
        }
        
        public bool CheckDishExist() => !Lock && Dish is not null;
    }
}