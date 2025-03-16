using UnityEngine;

namespace Input
{
    /// <summary>
    /// 控制所有輸入端的啟動與關閉，可以在這裡做細部的操作。
    /// </summary>
    public class InputSystem : MonoBehaviour
    {
        public static InputMap input;
        
        private void Awake()
        {
            input = new InputMap();
        }

        private void OnEnable()
        {
            input.Enable();
        }
        
        private void OnDisable()
        {
            input.Disable();
        }

        /// <summary>
        /// 滑鼠的位置，
        /// </summary>
        /// <returns></returns>
        public static Vector2 MousePos()
        {
            return input.Mouse.Position.ReadValue<Vector2>();
        }
    }
}
