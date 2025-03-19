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
        /// 滑鼠的左鍵是否一直被按住。
        /// </summary>
        /// <returns> 返還滑鼠的左鍵是否一直被按住。 </returns>
        public static bool MouseLeftButtonIsInProgress()
        {
            return input.Mouse.LeftClick.IsInProgress();
        }

        /// <summary>
        /// 滑鼠的位置，
        /// </summary>
        /// <returns> 回傳滑鼠當前的所在位置。 </returns>
        public static Vector2 MousePos()
        {
            return input.Mouse.Position.ReadValue<Vector2>();
        }

        /// <summary>
        /// 玩家移動的方向。
        /// </summary>
        /// <returns> 回傳玩家的移動方向。 </returns>
        public static Vector3 PlayerMoveDirection()
        {
            return input.Player.Move.ReadValue<Vector3>();
        }
    }
}
