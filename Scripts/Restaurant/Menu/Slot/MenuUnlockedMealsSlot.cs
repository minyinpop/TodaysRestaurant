using System;
using Database.Restaurant.Meals;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu.Slot
{
    // ==================================================
    // 用於在菜單裡，顯示玩家已解鎖料理的格子。
    // ==================================================
    
    public class MenuUnlockedMealsSlot : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        // 格子裡所儲存的料理資料。
        private MealsSO Meals { get; set; }
        
        
        
        // ========== { 格子顯示相關 } ==========
        
        [field: Tooltip("顯示料理的圖片組件。"), SerializeField]
        private Image MealsImage { get; set; }
        
        
        
        // ========== { 廣播事件 } ==========
        
        // 用於玩家點擊該格子後，所發出的廣播。
        // 會把該格子裡的料理資訊給傳出去。
        // 僅限於 MenuChosenMealsPage 做訂閱。
        public static event Action<MealsSO> OnUnlockedMealsSlotClicked;
        
        
        
        /// <summary>
        /// 當玩家點擊格子，就會觸發這個方法。
        /// 僅限於給 Button 組件的 On Click() 做訂閱。
        /// </summary>
        public void OnClicked()
        {
            OnUnlockedMealsSlotClicked?.Invoke(Meals);
        }
        
        
        
        /// <summary>
        /// 用於刷新玩家已解鎖料理的格子。
        /// </summary>
        /// <param name="newMeals"> 新傳入的料理資料。 </param>
        public void Refresh(MealsSO newMeals)
        {
            Meals = newMeals;
            MealsImage.sprite = Meals.Sprite;
        }
    }
}