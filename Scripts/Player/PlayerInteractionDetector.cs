using System.Collections.Generic;
using Interactable_Object.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;
using InputSystem = System.InputSystem;

namespace Player
{
    internal class PlayerInteractionDetector : MonoBehaviour
    {
        private InputManager Input { get; set; }

        private List<InteractableObjectBase> Interactables { get; set; } = new();

        private void Awake()
        {
            Input = InputSystem.Input;
            
            var info = $"\n遊戲物件：{gameObject.name}\n遊戲組件：{GetType().Name}\n";
            if (Tools.CheckNull(Input is null, $"找尋不到 InputSystem 組件！{info}")) enabled = false;
        }
        
        private void OnEnable() => Input.Player.Interact.performed += OnInteract;
        private void OnDisable() => Input.Player.Interact.performed -= OnInteract;
        private void OnDestroy() => Input.Player.Interact.Dispose();

        private void Update()
        {
            if (Interactables.Count == 0) return;
            Interactables.Sort((a, b) =>
            {
                var distanceA = Vector3.Distance(transform.position, a.transform.position);
                var distanceB = Vector3.Distance(transform.position, b.transform.position);
                return distanceA.CompareTo(distanceB);
            });
            for (var i = 0; i < Interactables.Count; i++)
            {
                if (i == 0) Interactables[i].Select();
                else Interactables[i].Deselect();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<InteractableObjectBase>(out var interactable)) return;
            Interactables.Add(interactable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<InteractableObjectBase>(out var interactable)) return;
            interactable.Deselect();
            Interactables.Remove(interactable);
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (Interactables.Count == 0) return;
            if (!Interactables[0].Interact()) return;
            Interactables.RemoveAt(0);
        }
    }
}