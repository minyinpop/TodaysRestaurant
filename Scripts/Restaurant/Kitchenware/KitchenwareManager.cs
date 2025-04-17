using UnityEngine;

namespace Restaurant.Kitchenware
{
    // ==================================================
    // 
    // ==================================================
    
    public class KitchenwareManager : MonoBehaviour
    {
        [field: Header("餐點選擇介面"), Tooltip("選擇要烹飪甚麼餐點的介面預製件。"), SerializeField]
        private GameObject CookingMenuPrefab { get; set; }
        
        [field: Tooltip("餐點選擇介面的生成位置。"), SerializeField]
        private Transform CookingMenuSpawnPoint { get; set; }
    }
}