using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input_System
{
    public partial class InputSystem
    {
        public static Vector2 MousePosition => _inputManager.Mouse.MousePosition.ReadValue<Vector2>();
        
        public static event Action OnClickedLeftButton;
        private static void OnLeftButtonClicked(InputAction.CallbackContext context)
        {
            OnClickedLeftButton?.Invoke();
        }
        
        public static event Action OnClickedRightButton;
        private static void OnRightButtonClicked(InputAction.CallbackContext context)
        {
            OnClickedRightButton?.Invoke();
        }
    }
}