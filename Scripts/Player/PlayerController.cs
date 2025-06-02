using Input;
using UnityEngine;

namespace Player
{
    internal class PlayerController : MonoBehaviour
    {
        [field: Header("必要組件")]
        [field: SerializeField] private PlayerAttribute Attribute { get; set; }
        [field: SerializeField] private Rigidbody Rig { get; set; }
        
        private InputManager Input { get; set; }
        private Vector3 MoveDir => Input.Player.Move.ReadValue<Vector3>();

        private void Awake()
        {
            Input = InputSystem.Input;

            CheckNull(Input is null, $"錯誤訊息：找尋不到 InputSystem。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n");
            CheckNull(!Attribute, $"錯誤訊息：Attribute 沒有被掛載。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n");
            CheckNull(!Rig, $"錯誤訊息：Rig 沒有被掛載。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n");
        }

        private void FixedUpdate()
        {
            Rig.linearVelocity = transform.InverseTransformDirection(MoveDir) * (Attribute.MoveSpeed * Time.fixedDeltaTime);
        }
        
        private void CheckNull(bool condition, string message)
        {
            if (!condition) return;
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
            enabled = false;
        }
    }
}