using UnityEngine;

namespace Database.Restaurant.Dish
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Dish", fileName = "New Data", order = 2)]
    public class DishSO : ScriptableObject
    {
        [field: Header("料理資訊設定")]
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        [field: Header("料理烹飪設定")]
        [field: SerializeField] public CookingUtensil CookingUtensil { get; private set; }
        
        [field: Header("料理基礎設定")]
        [field: SerializeField] public int Portion { get; private set; }
        [field: SerializeField] public float CookTime { get; private set; }
        [field: SerializeField] public float BurnTime { get; private set; }
    }
}