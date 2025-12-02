using System;

namespace Object.Bubble.Interface
{
    internal interface IBubble
    {
        public event Action OnClick;
        
        public void SetInteractable(bool interactable);
        
        public void CountDown(float time, Action onComplete);
    }
}