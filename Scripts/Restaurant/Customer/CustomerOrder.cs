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
    internal class CustomerOrder : MonoBehaviour
    {
        private AttributeSO Attribute { get; set; }
        private DishDeliverSO DishDeliver { get; set; }

        internal DishSO CurrentOrderDish { get; private set; }
        private List<DishSO> OrderDishes { get; set; } = new();
        
        private bool IsOrderAppetizer { get; set; }
        private bool IsOrderMainCourse { get; set; }
        private bool IsOrderDessert { get; set; }
        private bool IsOrderDrink { get; set; }
        
        private TodayDishSO TodayAppetizer { get; set; }
        private TodayDishSO TodayMainCourse { get; set; }
        private TodayDishSO TodayDessert { get; set; }
        private TodayDishSO TodayDrink { get; set; }

        public static event Func<DishSO> TryTakeDishEvent;
        
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

                if (Random.Range(0, 101) <= Attribute.OrderAttribute.OrderAppetizerChance)
                {
                    CurrentOrderDish = TodayAppetizer.OrderRandomDish();
                    OrderDishes.Add(CurrentOrderDish);
                    return CurrentOrderDish;
                }
            }

            if (!IsOrderMainCourse)
            {
                IsOrderMainCourse = true;
                
                if (Random.Range(0, 101) <= Attribute.OrderAttribute.OrderMainCourseChance)
                {
                    CurrentOrderDish = TodayMainCourse.OrderRandomDish();
                    OrderDishes.Add(CurrentOrderDish);
                    return CurrentOrderDish;
                }
            }

            if (!IsOrderDessert)
            {
                IsOrderDessert = true;
                
                if (Random.Range(0, 101) <= Attribute.OrderAttribute.OrderDessertChance)
                {
                    CurrentOrderDish = TodayDessert.OrderRandomDish();
                    OrderDishes.Add(CurrentOrderDish);
                    return CurrentOrderDish;
                }
            }

            if (!IsOrderDrink)
            {
                IsOrderDrink = true;
                
                if (Random.Range(0, 101) <= Attribute.OrderAttribute.OrderDrinkChance)
                {
                    CurrentOrderDish = TodayDrink.OrderRandomDish();
                    OrderDishes.Add(CurrentOrderDish);
                    return CurrentOrderDish;
                }
            }

            CurrentOrderDish = null;
            return CurrentOrderDish;
        }

        public DishSO TryTakeDish()
        {
            return TryTakeDishEvent?.Invoke();
        }
    }
}