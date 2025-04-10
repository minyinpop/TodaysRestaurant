using UnityEngine;

namespace Restaurant.Customer
{
    // ==================================================
    // 用於執行顧客點餐的程式碼。
    // ==================================================
    
    [RequireComponent(typeof(CustomerManager))]
    public class CustomerOrder : MonoBehaviour
    {
        // ========== { 狀態相關 } ==========
        
        // 顧客點餐的狀態陣列。
        private OrderStateEnum OrderState { get; set; } = OrderStateEnum.None;
        private enum OrderStateEnum
        {
            None,
            Ordering,
            WaitingForTheServer,
            WaitingForTheMeal,
        }
    }
}