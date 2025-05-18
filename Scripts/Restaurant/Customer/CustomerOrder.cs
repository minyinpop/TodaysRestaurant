using System;
using System.Collections.Generic;
using Database.Restaurant.Customer.Attribute;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using Database.Restaurant.Player.Dish_Deliver;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Restaurant.Customer
{
    public class CustomerOrder : MonoBehaviour
    {
        private AttributeSO Attribute { get; set; }
        private DishDeliverSO DishDeliver { get; set; }
        
        private bool IsOrderAppetizer { get; set; }
        private bool IsOrderMainCourse { get; set; }
        private bool IsOrderDessert { get; set; }
        private bool IsOrderDrink { get; set; }
        
        private List<DishSO> OrderDishes { get; set; } = new();
        
        private TodayDishSO TodayAppetizer { get; set; }
        private TodayDishSO TodayMainCourse { get; set; }
        private TodayDishSO TodayDessert { get; set; }
        private TodayDishSO TodayDrink { get; set; }

        public static event Func<DishSO> GetDishEvent;
        
        public void Init(AttributeSO attribute, DishDeliverSO deliver, TodayDishSO appetizer, TodayDishSO mainCourse, TodayDishSO dessert, TodayDishSO drink)
        {
            Attribute = attribute;
            DishDeliver = deliver;
            
            TodayAppetizer = appetizer;
            TodayMainCourse = mainCourse;
            TodayDessert = dessert;
            TodayDrink = drink;
        }

        public DishSO TryOrderDish()
        {
            if (!IsOrderAppetizer)
            {
                IsOrderAppetizer = true;

                if (Random.Range(0, 101) >= Attribute.OrderAttribute.OrderAppetizerChance)
                    return TodayAppetizer.TakeRandomDish();
                
                var getDish = TodayAppetizer.TakeRandomDish();
                OrderDishes.Add(getDish);
                return getDish;
            }

            return null;
        }

        public void GetDish(out DishSO dish)
        {
            dish = GetDishEvent?.Invoke();
        }
    }
}