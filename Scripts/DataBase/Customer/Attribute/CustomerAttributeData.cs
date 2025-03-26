using UnityEngine;

namespace DataBase.Customer.Attribute
{
    /// <summary>
    /// 用來儲存顧客的屬性以及狀態的資料庫。
    /// </summary>
    [CreateAssetMenu(fileName = "New Customer Attribute Data", menuName = "Customer Data/Attribute", order = 1)]
    public class CustomerAttributeData : ScriptableObject
    {
        [field: Header("移動設定"), Tooltip("顧客是否可以移動。"), SerializeField]
        public bool Moveable { get; private set; }
        
        [field: Tooltip("- 顧客的移動速度。\n- 使用 Transform 來做移動。"), SerializeField]
        public float MoveSpeed { get; private set; }
    }
}