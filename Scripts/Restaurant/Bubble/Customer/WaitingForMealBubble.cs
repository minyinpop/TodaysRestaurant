using System.Collections;
using Database.Restaurant.Meals;
using Restaurant.Customer;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Bubble.Customer
{
    // ==================================================
    // 顯示想要的餐點程式碼。
    // 在玩家幫顧客點完餐後，就會顯示顧客想要甚麼餐點，
    // ==================================================
    
    public class WaitingForMealBubble : MonoBehaviour
    {
        // ========== { 自身組件 } ==========

        [field: Header("自身組件"), Tooltip("- 自身的料理圖片。\n- 用於顯示顧客點了甚麼料理。"), SerializeField]
        private Image MealsImage { get; set; }
        
        [field: Tooltip("- 自身的遮罩圖片。\n- 用於顯示顧客的耐心剩下多少。"), SerializeField]
        private Image MaskImage { get; set; }
        
        // 用來管理顧客氣泡的組件。
        private CustomerOrder CustomerOrder { get; set; }
        
        // 暫存的料理資訊。
        // 用來顯示當前顧客想要的餐點是甚麼。
        private MealsSO ChooseMeals { get; set; }



        // ========== { 時間相關 } ==========
        
        // 顧客剩餘的耐心時間。
        private float RemainingPatienceTime { get; set; }
        
        
        
        // ========== { 異步協程 } ==========
        
        // 當前執行的異步協程。
        private IEnumerator CurrentCoroutine { get; set; }
        
        
        
        private void OnDisable()
        {
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }



        /// <summary>
        /// 當玩家點擊這個氣泡後，所執行的方法。
        /// 用於 Button 的 On Click() 做訂閱。
        /// </summary>
        public void OnClicked()
        {
        }
        
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerOrder"></param>
        /// <param name="randomPatienceTime"></param>
        /// <param name="chooseMeals"></param>
        public void OnInit(CustomerOrder customerOrder, MealsSO chooseMeals, float randomPatienceTime)
        {
            CustomerOrder = customerOrder;
            ChooseMeals = chooseMeals;
            RemainingPatienceTime = ChooseMeals.CookingTime + randomPatienceTime;
            
            MealsImage.sprite = ChooseMeals.Sprite;
            
            CurrentCoroutine = CountDownPatience();
            StartCoroutine(CurrentCoroutine);
        }
        
        
        
        /// <summary>
        /// 減少顧客耐心的異步協程。
        /// </summary>
        private IEnumerator CountDownPatience()
        {
            while (MaskImage.fillAmount < 1)
            {
                MaskImage.fillAmount += Time.deltaTime / RemainingPatienceTime;
                yield return null;
            }
            
            // TODO 執行顧客沒有耐心的邏輯 ......
        }
    }
}