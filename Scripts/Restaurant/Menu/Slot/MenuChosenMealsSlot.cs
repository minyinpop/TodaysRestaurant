using System;
using Database.Restaurant.Chosen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu.Slot
{
    // ==================================================
    // 用於在菜單裡，顯示玩家已選擇料理的格子。
    // ==================================================
    
    public class MenuChosenMealsSlot : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        // 格子裡所儲存的料理資料。
        public ChosenMealsSlot ChosenMealsSlot { get; private set; }
        
        
        
        // ========== { 格子顯示相關 } ==========
        
        [field: Tooltip("顯示料理的圖片組件。"), SerializeField]
        private Image MealsImage { get; set; }
        
        [field: Tooltip("顯示料理名稱與份數的文字組件。"), SerializeField]
        private TextMeshProUGUI MealsInfoTMP { get; set; }


        
        // ========== { 廣播事件 } ==========
        
        // 用於玩家點擊該格子後，所發出的廣播。
        // 會把本身的遊戲物件給傳出去。
        // 僅限於 MenuChosenMealsPage 做訂閱。
        public static event Action<GameObject> OnChosenMealsSlotClicked;
        
        
        
        /// <summary>
        /// 當玩家點擊格子，就會觸發這個方法。
        /// 僅限於給 Button 組件的 On Click() 做訂閱。
        /// </summary>
        public void OnClicked()
        {
            OnChosenMealsSlotClicked?.Invoke(gameObject);
        }
        
        
        
        /// <summary>
        /// 用於刷新玩家以選擇料理的格子。
        /// </summary>
        /// <param name="newChosenMealsSlot"> 新傳入的料理資料。 </param>
        public void Refresh(ChosenMealsSlot newChosenMealsSlot)
        {
            ChosenMealsSlot = newChosenMealsSlot;

            if (ChosenMealsSlot.Meals is null)
                return;
            
            MealsImage.sprite = ChosenMealsSlot.Meals.Sprite;
            MealsInfoTMP.text = $"{ChosenMealsSlot.Meals.Name} x{ChosenMealsSlot.Meals.Quantity}";
        }
    }
}