using System;
using Database.Restaurant.Menu;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Database.Restaurant.Customer.Attribute
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Customer/Attribute", fileName = "New Data", order = 1)]
    internal class AttributeSO : ScriptableObject
    {
        [field: Header("移動設定")]
        [field: SerializeField] public CustomerMoveAttribute MoveAttribute { get; set; }
        
        [field: Header("動畫設定")]
        [field: SerializeField] public CustomerAnimationAttribute AnimationAttribute { get; set; }
        
        [field: Header("點餐設定")]
        [field: SerializeField] public CustomerOrderAttribute OrderAttribute { get; set; }
    }

    [Serializable]
    internal class CustomerMoveAttribute
    {
        [field: SerializeField] public bool CanMove { get; set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
    }
    
    [Serializable]
    internal class CustomerAnimationAttribute
    {
        [field: SerializeField] private Range Blink { get; set; }

        public float GetRandomBlinkTime() => Random.Range(Blink.Min, Blink.Max);
    }

    [Serializable]
    internal class CustomerOrderAttribute
    {
        [field: Header("今日販售的料理資料")]
        [field: SerializeField] public TodayDishSO TodayAppetizer { get; private set; }
        [field: SerializeField] public TodayDishSO TodayMainCourse { get; private set; }
        [field: SerializeField] public TodayDishSO TodayDessert { get; private set; }
        [field: SerializeField] public TodayDishSO TodayDrink { get; private set; }
        
        [field: Header("各類餐點點餐機率")]
        [field: Range(0, 100), SerializeField] public int OrderAppetizerChance { get; private set; }
        [field: Range(0, 100), SerializeField] public int OrderMainCourseChance { get; private set; }
        [field: Range(0, 100), SerializeField] public int OrderDessertChance { get; private set; }
        [field: Range(0, 100), SerializeField] public int OrderDrinkChance { get; private set; }
        
        [field: Header("每個狀態的等待時間")]
        [field: SerializeField] private Range ThinkTime { get; set; }
        [field: SerializeField] private Range OrderTime { get; set; }
        [field: SerializeField] private Range WaitDishTime { get; set; }
        [field: SerializeField] private Range CheckoutTime { get; set; }
        
        public float GetRandomThinkTime() => Random.Range(ThinkTime.Min, ThinkTime.Max);
        public float GetRandomOrderTime() => Random.Range(OrderTime.Min, OrderTime.Max);
        public float GetRandomWaitDishTime() => Random.Range(WaitDishTime.Min, WaitDishTime.Max);
        public float GetRandomCheckoutTime() => Random.Range(CheckoutTime.Min, CheckoutTime.Max);
    }

    [Serializable]
    internal class Range
    {
        [field: SerializeField] public float Max { get; set; }
        [field: SerializeField] public float Min { get; set; }
    }
}