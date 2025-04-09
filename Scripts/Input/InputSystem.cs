using UnityEngine;

namespace Input
{
    // ==================================================
    // 用來管理輸入端的程式碼。
    // ==================================================
    
    public class InputSystem : MonoBehaviour
    {
        // 輸入端。
        public static InputManager Input { get; private set; }

        
        
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
        /// 用於判斷玩家是否按住走路鍵的方法。
        /// </summary>
        /// <returns></returns>
        public static bool IsWalkButtonPressed() => Input.Player.Walk.IsInProgress();
        
        
        
        /// <summary>
        /// 用於判斷玩家是否按住跑步鍵的方法。
        /// </summary>
        /// <returns> 回傳玩家是否按住。 </returns>
        public static bool IsRunButtonPressed() => Input.Player.Run.IsInProgress();
        
        
        
        /// <summary>
        /// 用於獲取玩家移動方向的方法。
        /// </summary>
        /// <returns> 回傳玩家的移動方向數值。 </returns>
        public static Vector3 MoveDirection() => Input.Player.Walk.ReadValue<Vector3>();
    }
}