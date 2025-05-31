using Input;
using UnityEngine;

namespace Player
{
    internal class PlayerController : MonoBehaviour
    {
        [field: Header("必要組件")]
        [field: SerializeField] private InputSystem InputSystem { get; set; }
        [field: SerializeField] private PlayerAttribute Attribute { get; set; }
        [field: SerializeField] private Rigidbody Rig { get; set; }
        
        private InputManager Input { get; set; }
        private Vector3 MoveDir => Input.Player.Move.ReadValue<Vector3>();

        private void Awake()
        {
            if (CheckNull(!InputSystem, "輸入系統 InputSystem 未被掛載。") ||
                CheckNull(!Attribute, "玩家屬性資料 Attribute 未被掛載。") ||
                CheckNull(!Rig, "玩家剛體資料 Rigidbody 未被掛載。")) return;
            Input = InputSystem.Input;
        }

        private void FixedUpdate()
        {
            Rig.linearVelocity = transform.InverseTransformDirection(MoveDir) * (Attribute.MoveSpeed * Time.fixedDeltaTime);
        }

        private bool CheckNull(bool condition, string message)
        {
            if (!condition) return false;
#if UNITY_EDITOR
            Debug.LogWarning($"錯誤訊息：{message}\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n");
#endif
            enabled = false;
            return true;
        }
    }
}