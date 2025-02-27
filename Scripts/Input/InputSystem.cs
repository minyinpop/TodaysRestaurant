using UnityEngine;

namespace Input
{
    /// <summary>
    /// 控制所有輸入端的啟動與關閉，可以在這裡做細部的操作。
    /// </summary>
    public class InputSystem : MonoBehaviour
    {
        private static InputMap _input;
        
        private void Awake()
        {
            _input = new InputMap();
        }

        private void OnEnable()
        {
            _input.Enable();
        }
        
        private void OnDisable()
        {
            _input.Disable();
        }
    }
}
