using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Player.Dish_Deliver;
using Restaurant.Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Player
{
    public class PlayerDishDeliver : MonoBehaviour
    {
        [field: Header("在運送料理的資料")]
        [field: SerializeField] private DishDeliverSO DishDeliver { get; set; }
        
        [field: Header("料理顯示的圖片組件")]
        [field: SerializeField] private List<Image> DishImages { get; set; }

        private void OnEnable()
        {
            KitchenwareManager.GetDishEvent += AddDish;
        }

        private void OnDestroy()
        {
            KitchenwareManager.GetDishEvent -= AddDish;
            
            for (var i = 0; i < DishDeliver.Dishes.Count; i++)
                DishDeliver.Dishes[i] = null;
        }

        private bool AddDish(DishSO dish)
        {
            for (var i = DishDeliver.Dishes.Count - 1; i >= 0; i--)
            {
                if (DishDeliver.Dishes[i] is not null)
                    continue;
                
                DishDeliver.Dishes[i] = dish;
                DishImages[i].gameObject.SetActive(true);
                DishImages[i].sprite = dish.Sprite;
                return true;
            }

            return false;
        }
    }
}