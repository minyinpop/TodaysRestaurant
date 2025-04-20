using System;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Database.Restaurant.Selected_Dish
{
    [Serializable]
    public class SelectedDishSlot
    {
        [field: SerializeField]
        public bool Locked { get; set; }
        
        [field: SerializeField]
        public DishSO Dish { get; set; }
        
        [field: SerializeField]
        public int Quantity { get; set; }
    }
}