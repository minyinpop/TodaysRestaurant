using System.Collections.Generic;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Database.Restaurant.Player.Unlock_Dish
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Player/Unlock Dish", fileName = "New Data", order = 1)]
    internal class UnlockDishSO : ScriptableObject
    {
        [field: Header("已解鎖料理的清單")]
        [field: SerializeField] public List<DishSO> UnlockDishes { get; set; }
    }
}