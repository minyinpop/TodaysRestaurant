using System;
using Database.Restaurant.Meals;
using UnityEngine;

namespace Database.Restaurant.Chosen
{
    // ==================================================
    // 玩家所選擇的料理格子資料庫。
    // 用於封裝格子的資訊，包含了是否上鎖、料理資訊等等 ......
    // ==================================================
    
    [Serializable]
    public struct ChosenMealsSlot
    {
        [field: Tooltip("是否上鎖？"), SerializeField]
        public bool IsLocked { get; set; }
        
        [field: Tooltip("料理資料。"), SerializeField]
        public MealsSO Meals { get; set; }
        
        [field: Tooltip("料理剩餘份數。"), SerializeField]
        public int Quantity { get; set; }
    }
}