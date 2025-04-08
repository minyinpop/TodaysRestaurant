using Database.Player.Meals;
using Database.Restaurant.Chosen;
using UnityEngine;

namespace Database.Restaurant.Menu
{
    // ==================================================
    // 用於儲存菜單頁面資料的資料庫。
    // 當前玩家所選擇的料理種類，所使用的 PlayerUnlockedMealsTypeSO 跟 ChosenMealsTypeSO，都會包含在這裏面。
    // 用於方便集中管理，以免開發者不小心把兩個頁面的資料庫給搞錯。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Menu Page", menuName = "Minyinpop/Restaurant/Menu Page", order = 1)]
    public class MenuPageSO : ScriptableObject
    {
        [field: Tooltip("已解鎖的料理資料庫。"), SerializeField]
        public PlayerUnlockedMealsTypeSO UnlockedMeals { get; private set; }
        
        [field: Tooltip("已選擇的料理資料庫。"), SerializeField]
        public ChosenMealsTypeSO ChosenMeals { get; private set; }
    }
}