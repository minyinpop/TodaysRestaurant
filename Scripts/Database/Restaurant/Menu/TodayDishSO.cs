using System.Collections.Generic;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Database.Restaurant.Menu
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Today's Dish", fileName = "New Data", order = 1)]
    internal class TodayDishSO : ScriptableObject
    {
        [field: Header("上架料理的清單")]
        [field: SerializeField] public List<TodayDishSlot> TodayDishSlots { get; set; }

        public DishSO OrderRandomDish()
        {
            List<TodayDishSlot> tempSlots = new();
            
            foreach (var slot in TodayDishSlots)
                tempSlots.Add(slot);

            while (tempSlots.Count > 0)
            {
                var slot = tempSlots[Random.Range(0, tempSlots.Count)];

                if (!slot.CheckDishExist())
                {
                    tempSlots.Remove(slot);
                    continue;
                }
                
                return slot.TakeDish();
            }

            return null;
        }
    }
}