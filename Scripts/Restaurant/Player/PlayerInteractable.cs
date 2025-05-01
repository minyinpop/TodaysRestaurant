using System.Collections.Generic;
using Interface;
using Restaurant.Kitchenware;
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
            
            KitchenwareManager.PlayerEnter += AddInteractable;
            KitchenwareManager.PlayerLeave += RemoveInteractable;
        }
        
        private void OnDisable()
        {
            Input.Player.Interact.started -= Interact;
            
            KitchenwareManager.PlayerEnter -= AddInteractable;
            KitchenwareManager.PlayerLeave -= RemoveInteractable;
        }

        private void AddInteractable(IPlayerInteractable interactable) => InteractableList.Add(interactable);

        private void RemoveInteractable(KitchenwareManager interactable) => InteractableList.Remove(interactable);

        private void Interact(InputAction.CallbackContext context) => InteractableList[0]?.Interact();
    }
}