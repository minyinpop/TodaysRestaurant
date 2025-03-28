using UnityEngine;

namespace DataBase.Item.Category.Cuisine
{
    /// <summary>
    /// 料理的數據，繼承 ItemData 抽象類。
    /// </summary>
    [CreateAssetMenu(fileName = "New Cuisine", menuName = "Item/Cuisine", order = 1)]
    public class Cuisine : ItemData
    {
        [field: Header("料理設定"), Tooltip("這道料理是用哪一個廚俱製作的。"), SerializeField]
        public KitchenwareTypeEnum KitchenwareType { get; set; } = KitchenwareTypeEnum.Uncategorized;
        public enum KitchenwareTypeEnum
        {
            Uncategorized,
            Stockpot,
            Drink
        }
        
        [field: Tooltip("- 在菜單的被選擇的蔡品的格子中，\n- 一格可以有多少這道料理上架。"), SerializeField]
        public int MenuQuantity { get; set; }
        
        [field: Tooltip("- 料理所需的烹飪時間，\n- 用於顧客的等待時間的基準值。"), SerializeField]
        public float CookTime { get; set; }
    }
}
