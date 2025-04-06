using System.Collections.Generic;
using UnityEngine;

namespace Database.Restaurant.Chosen
{
    // ==================================================
    // 玩家所選擇的料理資料庫。
    // 用於當天餐廳經營時，顧客所選擇的料理。
    // 裡面的資料開放給其它 class 做修改。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Chosen Meals Type", menuName = "Minyinpop/Restaurant/Chosen Meals Type", order = 2)]
    public class ChosenMealsTypeSO : ScriptableObject
    {
        [field: Tooltip("料理格資訊陣列。"), SerializeField]
        public List<ChosenMealsSlot> ChosenMealsList { get; set; }
    }
}