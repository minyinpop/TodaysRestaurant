using System;
using Object.Bubble.Interface;
using UnityEngine;

namespace Object.Bubble.Object
{
    internal sealed class BasicBubble : MonoBehaviour, IBubble
    {
        public event Action OnClick;
        
        public void SetInteractable(bool interactable)
        {
            //throw new NotImplementedException();
        }

        public void CountDown(float time, Action onComplete)
        {
            //throw new NotImplementedException();
        }
    }
}