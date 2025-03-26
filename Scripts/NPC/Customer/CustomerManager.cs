using DataBase.Customer.Wait;
using NPC.Customer.Bubble;
using UnityEngine;

namespace NPC.Customer
{
    /// <summary>
    /// 用來管理顧客行為邏輯的類。
    /// 需要 CustomerMove 與 CustomerAnimation 這兩個類的支援。
    /// </summary>
    [RequireComponent(typeof(CustomerMove))]
    [RequireComponent(typeof(CustomerAnimation))]
    public class CustomerManager : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("顧客的思考時間的資料庫。"), SerializeField]
        private CustomerWaitTimeData waitTimeData;
        
        [Header("點餐氣泡"), Tooltip("用於顧客點餐時，所冒出在頭上的點餐氣泡。"), SerializeField]
        private GameObject bubblePrefab;

        [Tooltip("用於當作生成點餐氣泡的位置。"), SerializeField]
        private Transform bubbleSpawnPoint;
        
        // 點餐氣泡的遊戲物件的暫存。
        private GameObject _bubble;
        
        // 自身的 CustomerMove 組件，用於顧客移動的類。
        private CustomerMove _customerMove;
        
        // 自身的 CustomerAnimation 組件，用於顧客的動畫的類。
        private CustomerAnimation _customerAnimation;
        
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
        }

        /// <summary>
        /// 當顧客走到位子旁邊後，就會觸發這個方法。
        /// </summary>
        public void OnSeat()
        {
            CustomerState = State.OnSeat;
            _customerAnimation.Seat();
            
            _bubble = Instantiate(bubblePrefab, bubbleSpawnPoint);
            _bubble.GetComponent<ThinkBubble>().InitBubble(waitTimeData);
        }
    }
}
