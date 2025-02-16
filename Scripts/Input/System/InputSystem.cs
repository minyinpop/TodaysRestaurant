using UnityEngine;

namespace Input.System
{
    public class InputSystem : MonoBehaviour
    {
        public static InputManager input;
        
        private void Awake() => input = new InputManager();
        private void OnEnable() => input.Enable();
        private void OnDisable() => input.Disable();
    }
}
