using System;

namespace Object.Bubble.Interface
{
    internal interface IBubble
    {
        public event Action OnClickBubble;
        
        public void SetInteractable(bool interactable);
        
        public void StartCountDown(float time, Action onComplete);
    }
}