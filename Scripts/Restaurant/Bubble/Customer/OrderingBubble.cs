using System.Collections;
using Restaurant.Customer;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Bubble.Customer
{
    // ==================================================
    // 準備好點餐的程式碼。
    // 顧客思考完後，就會呼叫玩家來點餐氣泡。
    // ==================================================
    
    public class OrderingBubble : MonoBehaviour
    {
        // ========== { 自身組件 } ==========
        
        [field: /*Header("自身組件"), */Tooltip("- 自身的遮罩圖片。\n- 用於顯示顧客的耐心剩下多少。"), SerializeField]
        private Image MaskImage { get; set; }
        
        // 用來管理顧客氣泡的組件。
        private CustomerBubble CustomerBubble { get; set; }
        
        
        
        // ========== { 時間相關 } ==========
        
        // 顧客剩餘的耐心時間。
        private float RemainingPatienceTime { get; set; }



        private void Update()
        {
            MaskImage.fillAmount += Time.deltaTime / RemainingPatienceTime;

            if (MaskImage.fillAmount >= 1)
            {
                // TODO 當顧客沒耐心後，所發生的事情。
            }
        }
        
        
        
        /// <summary>
        /// 當玩家點擊這個氣泡後，所執行的方法。
        /// </summary>
        public void OnClicked()
        {
            // TODO 寫 ......
        }
        
        
        
        public void OnInit(CustomerBubble customerBubble, float randomPatienceTime)
        {
            CustomerBubble = customerBubble;
            RemainingPatienceTime = randomPatienceTime;
        }
    }
}