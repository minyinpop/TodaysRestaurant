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
        
        [field: Header("Food Data")]
        [field: SerializeField] private List<DishSO> FoodsData;
        
        public void GetValues(out FoodTypeSO foodTypeData, out List<DishSO> foodsData)
        {
            foodTypeData = FoodTypeData;
            foodsData = FoodsData;
        }
    }
}