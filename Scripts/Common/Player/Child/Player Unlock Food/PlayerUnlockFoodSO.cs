using Common.Item.Food.Data.Food_Category;
using UnityEngine;

namespace Common.Player.Child.Player_Unlock_Food
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Unlock Food", fileName = "New Data")]
    internal sealed class PlayerUnlockFoodSO : ScriptableObject
    {
        [field: SerializeField] private FoodCategorySO[] UnlockFoods;
        public void GetUnlockFoods(out FoodCategorySO[] foods) => foods = UnlockFoods;
    }
}