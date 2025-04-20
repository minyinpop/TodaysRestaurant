using UnityEngine;

namespace Database.Restaurant.Dish
{
    [CreateAssetMenu(fileName = "New Data", menuName = "Minyinpop/Restaurant/Dish", order = 2)]
    public class DishSO : ScriptableObject
    {
        [field: Header("料理資訊"), SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public Sprite Sprite { get; private set; }



        [field: Header("數量設定"), Range(1, 30), SerializeField]
        public int MaxPortion { get; private set; }
    }
}