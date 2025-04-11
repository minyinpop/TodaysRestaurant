using UnityEngine;

namespace Restaurant.Customer
{
    // ==================================================
    // 用來管理顧客狀態的程式碼。
    // ==================================================
    
    [RequireComponent(typeof(CustomerPathfinding))]
    [RequireComponent(typeof(CustomerAnimator))]
    [RequireComponent(typeof(CustomerBubble))]
    [RequireComponent(typeof(CustomerOrder))]
    public class CustomerManager : MonoBehaviour
    {
        // ========== { 狀態相關 } ==========
        
        // 顧客在餐廳裡的狀態陣列。
        public CustomerStateEnum CustomerState { get; private set; } = CustomerStateEnum.SearchingForTheSeat;

        public enum CustomerStateEnum
        {
            SearchingForTheSeat,
            OnSeat,
            Leaving
        }



        // ========== { 自身組件 } ==========
        
        // 自身的 CustomerAnimator 組件。
        private CustomerAnimator CustomerAnimator { get; set; }
        
        // 自身的 CustomerBubble 組件。
        private CustomerBubble CustomerBubble { get; set; }
        
        
        
        public void Awake()
        {
            CustomerAnimator = GetComponent<CustomerAnimator>();
            CustomerBubble = GetComponent<CustomerBubble>();
        }
        
        
        
        public void ChangeState(CustomerStateEnum newCustomerState)
        {
            switch (newCustomerState)
            {
                case CustomerStateEnum.SearchingForTheSeat:
                {
                    break;
                }
                case CustomerStateEnum.OnSeat:
                {
                    CustomerAnimator.PlaySitAnimation();
                    CustomerBubble.InitThinkingBubble();
                    break;
                }
                case CustomerStateEnum.Leaving:
                {
                    break;
                }
            }
        }
    }
}