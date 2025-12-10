using System;
using Data.Item.Interface;

namespace Common.Clickable_Bubble.Interface
{
    internal interface IClickableBubble
    {
        public event Action OnClickBubble;
        
        public void SetInteractable(bool interactable);
        
        public void StartCountDown(float time, Action onComplete);
        public void StartCountDown(ITem item, float time, Action onComplete);
    }
}