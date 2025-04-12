using System.Collections;
using UnityEngine;

namespace Restaurant.Bubble.Customer
{
    // ==================================================
    // 準備好點餐的程式碼。
    // 顧客思考完後，就會呼叫玩家來點餐氣泡。
    // ==================================================
    
    public class OrderingBubble : MonoBehaviour
    {
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
        /// </summary>
        public void OnClicked()
        {
        }
        
        
        
        public void OnInit()
        {
            
        }
        
        
        
        // private IEnumerator CountDownPatience()
        // {}
    }
}