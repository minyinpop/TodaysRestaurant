using Database.Restaurant.Dish;
using UnityEngine;

namespace Restaurant.Kitchenware.Bubble
{
    public abstract class BubbleBase : MonoBehaviour
    {
        public virtual void PlayerEnter() {}
        
        public virtual void PlayerLeave() {}
        
        public virtual void OnClick() { }
        
        public virtual void Init(KitchenwareManager manager) { }

        public virtual void Init(KitchenwareManager manager, DishSO currentCookDish) { }
        
        public virtual void Init(KitchenwareCook cook) { }
    }
}