using UnityEngine;

namespace Input
{
    public class InputSystem : MonoBehaviour
    {
        // 裝置輸入端
        public static InputManager Input;
        
        private void Awake() { Input = new InputManager(); }
        private void OnEnable() { Input.Enable(); }
        private void OnDisable() { Input.Disable(); }
    }
}
