using System;
using Data.Item.Abstract;

namespace Object.Clickable_Bubble.Interface
{
    internal interface IClickableBubble
    {
        public event Action OnClickBubble;
        
        public void SetInteractable(bool interactable);
        
        public void StartCountDown(float time, Action onComplete);
        public void StartCountDown(ItemSO item, float time, Action onComplete);
    }
}