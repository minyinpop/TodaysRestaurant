using UnityEngine;

namespace System
{
    internal class InputSystem : MonoBehaviour
    {
        internal static InputManager Input { get; private set; }
        private void Awake() => Input = new InputManager();
        private void OnEnable() => Input.Enable();
        private void OnDisable() => Input.Disable();
        private void OnDestroy() => Input.Dispose();
    }
}