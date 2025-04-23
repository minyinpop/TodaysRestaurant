using UnityEngine;

namespace Database.Restaurant.Dish
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Dish", fileName = "New Data", order = 2)]
    public class DishSO : ScriptableObject
    {
        [field: Header("料理的基本資訊")]
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        [field: Header("料理上架後的份量")]
        [field: SerializeField] public int Portion { get; private set; }
    }
}