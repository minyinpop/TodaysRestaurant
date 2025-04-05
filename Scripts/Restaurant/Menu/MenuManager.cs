using Database.Restaurant.ChosenMeals;
using UnityEngine;

namespace Restaurant.Menu
{
    // ==================================================
    // 用於管理菜單的程式碼。
    // ==================================================
    public class MenuManager : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("被選擇的主菜料理資料庫。"), SerializeField]
        private ChosenMealsData mainCourseChosenMealsData;
        
        [Tooltip("被選擇的飲品料理資料庫。"), SerializeField]
        private ChosenMealsData drinksChosenMealsData;
        
        [Header("已解所料理的格子"), Tooltip("已解鎖的 料理選擇 格子預製件。"), SerializeField]
        private GameObject unlockedMealsSlotPrefab;
        
        [Tooltip("已上鎖的 料理選擇 格子預製件。"), SerializeField]
        private GameObject lockedMealsSlotPrefab;
        
        
        
        [Header("選擇料理的格子"), Tooltip("已解鎖且有 選擇料理 的格子預製件。"), SerializeField]
        private GameObject unlockedChosenMealsSlotWithMealsPrefab;
        
        [Tooltip("已解鎖但沒有 選擇料理 的格子預製件。"), SerializeField]
        private GameObject unlockedChosenMealsSlotWithoutMealsPrefab;
        
        [Tooltip("未解鎖的 選擇料理 的格子預製件。"), SerializeField]
        private GameObject lockedChosenMealsSlotPrefab;
        
        
        
        /// <summary>
        /// 當菜單開啟按鈕被點下時，所執行的方法。
        /// 用於 Button 的 On Click() 訂閱。
        /// </summary>
        public void OnOpenButtonPressed()
        {
        }
    }
}