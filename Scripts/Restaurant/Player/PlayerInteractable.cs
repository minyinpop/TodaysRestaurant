using System.Collections.Generic;
using Interface;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Restaurant.Player
{
    public class PlayerInteractable : MonoBehaviour
    {
        private InputManager Input { get; set; }
        
        private List<IPlayerInteractable> InteractableList { get; set; } = new();
        
        private void Awake() => Input = InputSystem.Input;

        private void OnEnable()
        {
            Input.Player.Interact.started += Interact;
            
            IPlayerInteractable.PlayerEnterEvent += AddInteractable;
            IPlayerInteractable.PlayerLeaveEvent += RemoveInteractable;
        }
        
        private void OnDisable()
        {
            Input.Player.Interact.started -= Interact;
            
            IPlayerInteractable.PlayerEnterEvent -= AddInteractable;
            IPlayerInteractable.PlayerLeaveEvent -= RemoveInteractable;
        }

        private void AddInteractable(IPlayerInteractable interactable) => InteractableList.Add(interactable);

        private void RemoveInteractable(IPlayerInteractable interactable) => InteractableList.Remove(interactable);

        private void Interact(InputAction.CallbackContext context)
        {
            if (InteractableList.Count <= 0)
                return;
            
            InteractableList[0]?.Interact();
        }
    }
}