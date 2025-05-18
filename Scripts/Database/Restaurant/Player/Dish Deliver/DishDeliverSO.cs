using System.Collections.Generic;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Database.Restaurant.Player.Dish_Deliver
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Player/Deliver Dish", fileName = "New Data", order = 3)]
    public class DishDeliverSO : ScriptableObject
    {
        [field: Header("正在運送的料理資料")]
        [field: SerializeField] public List<DishSO> Dishes { get; private set; }
    }
}