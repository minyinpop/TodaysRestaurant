using System;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Database.Restaurant.Menu
{
    [Serializable]
    public class TodayDishSlot
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

        public bool TakeDish()
        {
            if (Lock || Dish is null || Portion <= 0)
                return false;
            
            Portion -= 1;
            
            if (Portion <= 0)
                ClearData();
            
            return true;
        }

        public void ClearData()
        {
            Lock = false;
            Dish = null;
            Portion = 0;
        }
        
        public bool CheckDishExist() => !Lock && Dish is not null;
    }
}