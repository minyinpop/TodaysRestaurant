using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using Database.Restaurant.Player.Dish_Deliver;
using UnityEngine;

namespace Restaurant.Customer
{
    public class CustomerOrder : MonoBehaviour
    {
        private DishDeliverSO DishDeliver { get; set; }
        
        private bool IsTakeAppetizer { get; set; }
        private bool IsTakeMainCourse { get; set; }
        private bool IsTakeDessert { get; set; }
        private bool IsTakeDrink { get; set; }
        
        private TodayDishSO TodayAppetizer { get; set; }
        private TodayDishSO TodayMainCourse { get; set; }
        private TodayDishSO TodayDessert { get; set; }
        private TodayDishSO TodayDrink { get; set; }

        private List<DishSO> ChooseDishes { get; set; } = new();
        
        public void Init(DishDeliverSO deliver, TodayDishSO appetizer, TodayDishSO mainCourse, TodayDishSO dessert, TodayDishSO drink)
        {
            DishDeliver = deliver;
            
            TodayAppetizer = appetizer;
            TodayMainCourse = mainCourse;
            TodayDessert = dessert;
            TodayDrink = drink;
        }

        public void GetDish(out DishSO selectDish)
        {
            // TODO 給予玩家頭上的料理
            selectDish = null;
        }
    }
}