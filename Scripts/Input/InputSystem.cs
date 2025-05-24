using UnityEngine;

namespace Input
{
    internal class InputSystem : MonoBehaviour
    {
        public static InputManager Input { get; private set; }

        private void Awake() => Input = new InputManager();

        public void OnEnable() => Input.Enable();
        
        public void OnDisable() => Input.Disable();
    }
}