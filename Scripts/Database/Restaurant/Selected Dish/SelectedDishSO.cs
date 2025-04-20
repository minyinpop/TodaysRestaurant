using System.Collections.Generic;
using UnityEngine;

namespace Database.Restaurant.Selected_Dish
{
    [CreateAssetMenu(fileName = "New Data", menuName = "Minyinpop/Restaurant/Selected Dish", order = 1)]
    public class SelectedDishSO : ScriptableObject
    {
        [field: SerializeField]
        public List<SelectedDishSlot> SelectedDishSlotList { get; set; }
    }
}