using System;
using System.Collections.Generic;
using Data.General.Enum;
using Data.Item.Type.Dish;
using UnityEngine;

namespace Data.General
{
    [Serializable]
    internal sealed class DishCategory
    {
        [field: Header("Dish Type")]
        [field: SerializeField] private FoodType FoodType;

        [field: Header("Dish Data")]
        [field: SerializeField] private List<DishSO> DishData;
        
        public void GetValues(out FoodType type, out List<DishSO> data)
        {
            type = FoodType;
            data = DishData;
        }
    }
}