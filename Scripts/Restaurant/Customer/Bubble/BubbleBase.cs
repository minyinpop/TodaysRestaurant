using System;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Restaurant.Customer.Bubble
{
    public abstract class BubbleBase : MonoBehaviour
    {
        public event Action ClickBubble;
        public event Action CustomerHaveNoPatience;
        
        public virtual void Init(float time) { }
        public virtual void Init(float time, DishSO dish) { }
        
        protected void OnClickBubble()
        {
            ClickBubble?.Invoke();
            Destroy(gameObject);
        }
        
        protected void OnCustomerHaveNoPatience() => CustomerHaveNoPatience?.Invoke();
    }
}