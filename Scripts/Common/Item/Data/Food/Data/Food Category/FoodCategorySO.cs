using System.Collections.Generic;
using Common.Item.Data.Food.Data.Food_Type;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Item.Data.Food.Data.Food_Category
{
    [CreateAssetMenu(menuName = "Minyinpop/Food/Food Category", fileName = "New Data")]
    internal sealed class FoodCategorySO : ScriptableObject
    {
        [field: Header("Food Type")]
        [field: SerializeField, FormerlySerializedAs("FoodTypeData")] private FoodTypeSO foodTypeData;
                                                                              public FoodTypeSO FoodTypeData => foodTypeData;
        
        [field: Header("Food Data")]
        [field: SerializeField, FormerlySerializedAs("FoodsData")] private List<FoodSO> foodsData;
                                                                           public IReadOnlyList<FoodSO> FoodsData => foodsData;
    }
}