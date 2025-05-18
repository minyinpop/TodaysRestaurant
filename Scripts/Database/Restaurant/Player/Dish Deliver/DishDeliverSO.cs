using System.Collections.Generic;
using Database.Restaurant.Dish;
using UnityEngine;
using UnityEngine.UI;

namespace Database.Restaurant.Player.Dish_Deliver
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Player/Deliver Dish", fileName = "New Data", order = 3)]
    internal class DishDeliverSO : ScriptableObject
    {
        [field: Header("正在運送的料理資料")]
        [field: SerializeField] public List<DishSO> Dishes { get; private set; }

        public bool AddDish(List<Image> dishImages, DishSO dish)
        {
            for (var i = Dishes.Count - 1; i >= 0; i--)
            {
                if (Dishes[i] is not null)
                    continue;

                dishImages[i].gameObject.SetActive(true);
                dishImages[i].sprite = dish.Sprite;
                
                Dishes[i] = dish;
                return true;
            }

            return false;
        }

        public DishSO TryTakeDish()
        {
            for (var i = Dishes.Count - 1; i >= 0; i--)
            {
                if (Dishes[i] is null)
                    continue;

                var dish = Dishes[i];
                Dishes[i] = null;
                return dish;
            }

            return null;
        }

        public void Clear()
        {
            for (var i = 0; i < Dishes.Count; i++)
                Dishes[i] = null;
        }

        public bool CheckDishExist()
        {
            foreach (var dish in Dishes)
            {
                if (dish is not null)
                    return true;
            }

            return false;
        }
    }
}