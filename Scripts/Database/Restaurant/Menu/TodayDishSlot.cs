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

        public void ClearData()
        {
            Lock = false;
            Dish = null;
            Portion = 0;
        }
    }
}