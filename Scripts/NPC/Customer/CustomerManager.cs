using DataBase.Customer.Wait;
using UnityEngine;

namespace NPC.Customer
{
    /// <summary>
    /// 用來管理顧客行為邏輯的類。
    /// 需要 CustomerMove 與 CustomerAnimation 這兩個類的支援。
    /// </summary>
    [RequireComponent(typeof(CustomerMove))]
    [RequireComponent(typeof(CustomerAnimation))]
    [RequireComponent(typeof(CustomerOrder))]
    [RequireComponent(typeof(CustomerOrderBubble))]
    public class CustomerManager : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("顧客的思考時間的資料庫。"), SerializeField]
        private CustomerWaitTimeData waitTimeData;
        
        
        
        // 自身的 CustomerMove 組件，用於顧客移動的類。
        private CustomerMove _customerMove;
        
        // 自身的 CustomerAnimation 組件，用於顧客的動畫的類。
        private CustomerAnimation _customerAnimation;
        
        // 自身的 CustomerOrder 組件，用於顧客點餐的類。
        private CustomerOrder _customerOrder;
        
        // 自身的 CustomerOrderBubble 組件，用於顧客的點餐氣泡的類。
        private CustomerOrderBubble _customerOrderBubble;
        
        
        
        // 顧客當前的狀態。
        public State CustomerState { get; private set; } = State.GoToSeat;

        public enum State
        {
            GoToSeat,
            OnSeat,
            Leaving
        };

        private void Awake()
        {
            _customerMove = GetComponent<CustomerMove>();
            _customerAnimation = GetComponent<CustomerAnimation>();
            _customerOrder = GetComponent<CustomerOrder>();
            _customerOrderBubble = GetComponent<CustomerOrderBubble>();
        }
        
        /// <summary>
        /// 當顧客走到位子旁邊後，就會觸發這個方法。
        /// </summary>
        public void OnSeat()
        {
            CustomerState = State.OnSeat;
            
            _customerAnimation.Seat();
            _customerOrder.OnStateChange(CustomerOrder.State.Thinking);
        }
    }
}
