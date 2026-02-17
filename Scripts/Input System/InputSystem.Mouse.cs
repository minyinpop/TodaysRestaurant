using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input_System
{
    public partial class InputSystem
    {
        // Property
        public static event Action OnClickedLeftButton;
        public static event Action OnClickedRightButton;

        public static Vector2 MousePosition => _inputManager.Mouse.MousePosition.ReadValue<Vector2>();
        
        private static void OnLeftButtonClicked(InputAction.CallbackContext context)
        {
            OnClickedLeftButton?.Invoke();
        }
        
        private static void OnRightButtonClicked(InputAction.CallbackContext context)
        {
            OnClickedRightButton?.Invoke();
        }
    }
}