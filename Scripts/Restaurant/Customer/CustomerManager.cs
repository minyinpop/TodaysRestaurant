using UnityEngine;

namespace Restaurant.Customer
{
    // ==================================================
    // 用來管理顧客狀態的程式碼。
    // ==================================================
    
    [RequireComponent(typeof(CustomerBubble))]
    [RequireComponent(typeof(CustomerPathfinding))]
    public class CustomerManager : MonoBehaviour
    {
        // ========== { 狀態相關 } ==========
        
        // 顧客在餐廳裡的狀態陣列。
        private CustomerStateEnum CustomerState { get; set; } = CustomerStateEnum.None;
        private enum CustomerStateEnum
        {
            None,
            SearchingForTheSeat,
            OnSeat,
        }
    }
}