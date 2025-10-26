using Data.Item.Base;
using UnityEngine;

namespace System.Economy.Child.Customer.Child.Bubble
{
    internal abstract class Bubble : MonoBehaviour
    {
        public virtual void SetInteractable(bool interactable) { }

        public event Action OnClick;
        protected void OnClicked() { OnClick?.Invoke(); }
        
        public virtual void ShowItem(ItemSO item, float time, Action onComplete) { }
        public virtual void CountDown(float time, Action onComplete) { }
    }
}