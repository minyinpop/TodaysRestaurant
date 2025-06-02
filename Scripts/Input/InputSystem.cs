using UnityEngine;

namespace Input
{
    internal class InputSystem : MonoBehaviour
    {
        public static InputManager Input { get; private set; }
        private void Awake() => Input = new InputManager();
        private void OnEnable() => Input.Enable();
        private void OnDisable() => Input.Disable();
    }
}