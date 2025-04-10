using UnityEngine;

namespace Database.Restaurant.Customer.Path
{
    // ==================================================
    // 用來當作顧客路徑點的資料。
    // 包括移動、到位置點、離開等等。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Customer Path", menuName = "Minyinpop/Restaurant/Customer/Path", order = 2)]
    public class CustomerPathSO : ScriptableObject
    {
        [field: Header("路徑"), Tooltip("顧客的路徑點陣列。"), SerializeField]
        public Vector3[] PathList { get; private set; }
        
        [field: Header("座位點"), Tooltip("目標座位的位置點。"), SerializeField]
        public Vector3 SeatPosition { get; private set; }
    }
}