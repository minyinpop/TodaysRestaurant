using System.Collections.Generic;
using Database.Restaurant.Dish;
using Restaurant.Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Player
{
    public class PlayerDishDeliver : MonoBehaviour
    {
        [field: Header("料理顯示的圖片組件")]
        [field: SerializeField] private List<Image> DishImages { get; set; }

        private List<DishSO> Dishes { get; set; } = new();

        private void Start()
        {
            for (var i = 0; i < DishImages.Count; i++)
                Dishes.Add(null);
        }

        private void OnEnable()
        {
            KitchenwareManager.GetDishEvent += AddDish;
        }

        private void OnDestroy()
        {
            KitchenwareManager.GetDishEvent -= AddDish;
        }

        private bool AddDish(DishSO dish)
        {
            for (var i = Dishes.Count - 1; i >= 0; i--)
            {
                if (Dishes[i] is not null)
                    continue;
                
                Dishes[i] = dish;
                DishImages[i].gameObject.SetActive(true);
                DishImages[i].sprite = dish.Sprite;
                return true;
            }

            return false;
        }
    }
}