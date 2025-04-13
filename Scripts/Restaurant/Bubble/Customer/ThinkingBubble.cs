using UnityEngine;

namespace Restaurant.Bubble.Customer
{
    // ==================================================
    // 思考餐點氣泡的程式碼。
    // 顧客在思考要點甚麼餐點的氣泡。
    // ==================================================
    
    public class ThinkingBubble : MonoBehaviour
    {
        /// <summary>
        /// 當外部程式碼呼叫這個方法時，開始倒數，時間到就會刪除該氣泡。
        /// </summary>
        /// <param name="deleteTime"> 用於刪除氣泡的倒數時間。 </param>
        public void OnInit(float deleteTime)
        {
            Destroy(gameObject, deleteTime);
        }
    }
}