using System.Collections.Generic;
using Database.Restaurant.Meals;
using UnityEngine;

namespace Database.Player.Meals
{
    // ==================================================
    // 玩家已解鎖料理的資料庫。
    // 依照料理的種類做區分，每一個新種類的料理，就要創建一筆資料庫。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Unlocked Meals Type", menuName = "Minyinpop/Player/Meals/Unlocked Meals Type")]
    public class PlayerUnlockedMealsTypeSO : ScriptableObject
    {
        [field: Tooltip("已解鎖的料理。"), SerializeField]
        public List<MealsSO> UnlockedMealsList { get; private set; }
    }
}