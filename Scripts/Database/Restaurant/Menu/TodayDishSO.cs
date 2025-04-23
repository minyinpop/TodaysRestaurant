using System.Collections.Generic;
using UnityEngine;

namespace Database.Restaurant.Menu
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Today's Dish", fileName = "New Data", order = 1)]
    public class TodayDishSO : ScriptableObject
    {
        [field: Header("上架料理的清單")]
        [field: SerializeField] public List<TodayDishSlot> TodayDishSlots { get; set; }
    }
}