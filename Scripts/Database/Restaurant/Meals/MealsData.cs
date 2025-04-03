using UnityEngine;

namespace Database.Restaurant.Meals
{
    // ==================================================
    // 用於當作料理的資料庫。
    // ==================================================
    [CreateAssetMenu(fileName = "New Meals Data", menuName = "Today's Restaurant/Meals Data", order = 1)]
    public class MealsData : ScriptableObject
    {
        [field: Header("資訊設定"), Tooltip("料理的圖片。"), SerializeField]
        public Sprite MealsSprite { get; private set; }
        
        [field: Tooltip("料理的名稱。"), SerializeField]
        public string MealsName { get; private set; }
        
        [field: Header("遊玩設定"), Tooltip("料理的份數。"), SerializeField]
        public int MealsQuantity { get; private set; }
    }
}