using UnityEngine;

namespace Input
{
    // ==================================================
    // 用來管理輸入端的程式碼。
    // ==================================================
    
    public class InputSystem : MonoBehaviour
    {
        // 輸入端。
        private static InputManager Input { get; set; }

        
        
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

        
        
        /// <summary>
        /// 用於獲取玩家移動方向的方法。
        /// </summary>
        /// <returns> 回傳玩家的移動方向數值。 </returns>
        public Vector3 MoveDirection() => Input.Player.Move.ReadValue<Vector3>();
    }
}