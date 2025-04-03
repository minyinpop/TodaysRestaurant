using System;
using Database.Restaurant.Meals;
using UnityEngine;

namespace Database.Restaurant.ChosenMeals
{
    // ==================================================
    // 用於當作玩家所選擇的料理的格子。
    // ==================================================
    [Serializable]
    public struct ChosenMealsStruct
    {
        [field: Header("選擇格的資料"), Tooltip("料理的資料。"), SerializeField]
        public MealsData MealsData { get; private set; }
        
        [field: Tooltip("- 當前的料理份數，\n- 當天還剩下多少份。"), SerializeField]
        public int MealsQuantity { get; private set; }
    }
}