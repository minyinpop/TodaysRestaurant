using Input;
using UnityEngine;
using Utility;

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

            if (Tools.CheckNull(Input is null, $"錯誤訊息：找尋不到 InputSystem。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n") ||
                Tools.CheckNull(!Attribute, $"錯誤訊息：Attribute 未被掛載。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n") ||
                Tools.CheckNull(!Rig, $"錯誤訊息：Rig 未被掛載。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n")) enabled = false;
        }

        private void FixedUpdate()
        {
            Rig.linearVelocity = transform.InverseTransformDirection(MoveDir) * (Attribute.MoveSpeed * Time.fixedDeltaTime);
        }
    }
}