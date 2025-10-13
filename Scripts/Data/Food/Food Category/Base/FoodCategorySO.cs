using System.Collections.Generic;
using Data.Food.Food_Type.Base;
using Data.Item.Type.Dish;
using UnityEngine;

namespace Data.Food.Food_Category.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Food/Food Category", fileName = "New Data")]
    internal sealed class FoodCategorySO : ScriptableObject
    {
        [field: Header("Food Type")]
        [field: SerializeField] private FoodTypeSO FoodTypeData;
        
        [field: Header("Dish Data")]
        [field: SerializeField] private List<DishSO> DishData;
        
        public void GetValues(out FoodTypeSO foodTypeData, out List<DishSO> dishesData)
        {
            foodTypeData = FoodTypeData;
            dishesData = DishData;
        }
    }
}