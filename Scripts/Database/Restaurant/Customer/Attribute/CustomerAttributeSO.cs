using UnityEngine;

namespace Database.Restaurant.Customer.Attribute
{
    // ==================================================
    // 顧客的屬性資料。
    // 裡面的資料開放更改，請注意使用。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Customer Path", menuName = "Minyinpop/Restaurant/Customer/Attribute", order = 1)]
    public class CustomerAttributeSO : ScriptableObject
    {
        // ========== { 移動相關 } ==========
        
        [field: Header("移動設定"), Tooltip("- 基本的移動速度。\n- 使用物理引擎來移動。"), SerializeField]
        public float BasicMoveSpeed { get; set; }
    }
}