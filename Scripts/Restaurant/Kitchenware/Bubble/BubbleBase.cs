using UnityEngine;

namespace Restaurant.Kitchenware.Bubble
{
    public abstract class BubbleBase : MonoBehaviour
    {
        public virtual void PlayerEnter() {}
        
        public virtual void PlayerLeave() {}
        
        public virtual void Init(KitchenwareManager manager) { }

        public virtual void OnClick() { }
    }
}