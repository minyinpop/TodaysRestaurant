using System.Collections.Generic;
using UnityEngine;

namespace Database.Restaurant.ChosenMeals
{
    // ==================================================
    // 用於當作玩家選擇的料理的資料庫。
    // 每一個種類的料理就要生成一個資料庫。
    // ==================================================
    [CreateAssetMenu(fileName = "New Chosen Meals Data", menuName = "Today's Restaurant/Chosen Meals Data", order = 2)]
    public class ChosenMealsData : ScriptableObject
    {
        [Header("料理資料"), Tooltip("- 這個類別的料理，\n- 擁有甚麼料理的資料？")]
        public List<ChosenMealsStruct> mealsDataList;
    }
}