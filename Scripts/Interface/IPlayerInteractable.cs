using System;
using UnityEngine;

namespace Interface
{
    public abstract class IPlayerInteractable : MonoBehaviour
    {
        public static event Action<IPlayerInteractable> PlayerEnterEvent;
        public static event Action<IPlayerInteractable> PlayerLeaveEvent;

        protected void PlayerEnter(IPlayerInteractable interactable) => PlayerEnterEvent?.Invoke(interactable);
        
        protected void PlayerLeave(IPlayerInteractable interactable) => PlayerLeaveEvent?.Invoke(interactable);
        
        public abstract void Interact();
    }
}