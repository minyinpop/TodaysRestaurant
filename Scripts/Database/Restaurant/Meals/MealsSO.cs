using UnityEngine;

namespace Database.Restaurant.Meals
{
    // ==================================================
    // 料理的資料庫。
    // 只限用於餐廳經營時的料理資料。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Meals", menuName = "Minyinpop/Restaurant/Meals", order = 3)]
    public class MealsSO : ScriptableObject
    {
        [field: Header("料理資訊"), Tooltip("料理名稱"), SerializeField]
        public string Name { get; private set; }
        
        [field: Tooltip("料理的圖片。"), SerializeField]
        public Sprite Sprite { get; private set; }
        
        [field: Tooltip("料理的份數。"), SerializeField]
        public int Quantity { get; private set; }
    }
}