using UnityEngine;

namespace Input.Custom
{
    /// <summary>
    /// 用來集合所有輸入端的操作。
    /// 可以從這裡做一些細部的操作。
    /// </summary>
    public class InputSystem : MonoBehaviour
    {
        public static InputManager Input;

        private void Awake()
        {
            Input = new InputManager();
        }

        private void OnEnable()
        {
            Input.Enable();
        }
        
        private void OnDisable()
        {
            Input.Disable();
        }
    }
}
