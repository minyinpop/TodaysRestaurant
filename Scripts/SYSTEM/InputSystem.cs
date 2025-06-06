using UnityEngine;

namespace SYSTEM
{
    internal class InputSystem : MonoBehaviour
    {
        public static InputManager Input { get; set; }
        private void Awake() => Input = new InputManager();
        private void OnEnable() => Input.Enable();
        private void OnDisable() => Input.Disable();
        private void OnDestroy() => Input.Dispose();
    }
}