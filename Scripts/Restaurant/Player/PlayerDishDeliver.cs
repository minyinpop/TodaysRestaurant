using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Player.Dish_Deliver;
using Restaurant.Customer;
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
            CustomerOrder.GetDishEvent += GetDish;
        }

        private void OnDestroy()
        {
            KitchenwareManager.GetDishEvent -= AddDish;
            CustomerOrder.GetDishEvent -= GetDish;

            DishDeliver.Clear();
        }

        private bool AddDish(DishSO dish)
        {
            return DishDeliver.AddDish(DishImages, dish);
        }

        private DishSO GetDish()
        {
            DishDeliver.GetDish(out var dish);
            return dish;
        }
    }
}