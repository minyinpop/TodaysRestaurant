using Item.Data.Food.Data.Food_Category;
using UnityEngine;

namespace Player_System.Data.Child.Unlock_Food
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Unlock Food Data", fileName = "New Data")]
    internal sealed class UnlockFoodSO : ScriptableObject
    {
        [field: SerializeField] private FoodCategorySO[] UnlockFoods;
        public void GetUnlockFoods(out FoodCategorySO[] foods) => foods = UnlockFoods;
    }
}