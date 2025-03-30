using UnityEngine;

namespace DataBase.Customer.Wait
{
    /// <summary>
    /// 當作顧客等待時間的資料庫。
    /// </summary>
    [CreateAssetMenu(fileName = "New Customer Wait Time Data", menuName = "Customer Data/Wait Time", order = 2)]
    public class CustomerWaitTimeData : ScriptableObject
    {
        [field: Header("思考的設定"), Tooltip("顧客的最長的思考時間。"), SerializeField]
        public float MaxThinkTime { get; private set; }
        
        [field: Tooltip("顧客的最短的思考時間。"), SerializeField]
        public float MinThinkTime { get; private set; }
        
        
        
        [field: Header("點餐的設定"), Tooltip("顧客的最長的點餐的等待時間。"), SerializeField]
        public float MaxWaitOrderTime { get; private set; }
        
        [field: Tooltip("顧客的最短的點餐的等待時間。"), SerializeField]
        public float MinWaitOrderTime { get; private set; }
        
        
        
        [field: Header("等餐的設定"), Tooltip("- 顧客的最長的等待餐點的時間。\n- 會加上料理的烹飪時間。"), SerializeField]
        public float MaxWaitCuisineTime { get; private set; }
        
        [field: Tooltip("- 顧客的最短的等待餐點的時間。\n- 會加上料理的烹飪時間。"), SerializeField]
        public float MinWaitCuisineTime { get; private set; }
        
        
        
        [field: Header("用餐設定"), Tooltip("- 顧客最長的用餐時間。\n- 會加上料理的用餐時間。"), SerializeField]
        public float MaxEatTime { get; private set; }
        
        [field: Tooltip("- 顧客最短的用餐時間。\n- 會加上料理的用餐時間。"), SerializeField]
        public float MinEatTime { get; private set; }
        
        
        
        [field: Header("等結帳的設定"), Tooltip("顧客的最長的等待結帳的時間。"), SerializeField]
        public float MaxCheckoutTime { get; private set; }
        
        [field: Tooltip("顧客的最短的等待結帳的時間。"), SerializeField]
        public float MinCheckoutTime { get; private set; }
    }
}
