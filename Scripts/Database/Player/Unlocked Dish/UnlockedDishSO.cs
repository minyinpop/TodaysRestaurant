using System.Collections.Generic;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Database.Player.Unlocked_Dish
{
    [CreateAssetMenu(fileName = "New Data", menuName = "Minyinpop/Player/Unlocked Dish", order = 1)]
    public class UnlockedDishSO : ScriptableObject
    {
        [field: SerializeField]
        public List<DishSO> UnlockedDishList { get; set; }
    }
}