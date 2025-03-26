using DataBase.Customer.Wait;
using UnityEngine;

namespace NPC.Customer.Bubble
{
    public abstract class ThinkBubble : MonoBehaviour
    {
        /// <summary>
        /// 初始化點餐氣泡用的方法。
        /// </summary>
        /// <param name="newWaitTimeData"> 新傳入的顧客的等待資料。 </param>
        public abstract void InitBubble(CustomerWaitTimeData newWaitTimeData);
    }
}
