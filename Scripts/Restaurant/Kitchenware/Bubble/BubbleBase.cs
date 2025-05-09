using System;
using Restaurant.Kitchenware.Cook_Menu;
using UnityEngine;

namespace Restaurant.Kitchenware.Bubble
{
    public abstract class BubbleBase : MonoBehaviour
    {
        public event Action OnClickEvent;
        
        public virtual void PlayerEnter() { }
        
        public virtual void PlayerLeave() { }
        
        protected void OnClick() => OnClickEvent?.Invoke();
        
        public virtual void Init(KitchenwareCookMenu cookMenu) { }
    }
}