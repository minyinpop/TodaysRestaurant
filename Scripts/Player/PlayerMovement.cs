using System;
using Player.Attribute;
using UnityEngine;
using Utility;

namespace Player
{
    internal class PlayerMovement : MonoBehaviour
    {
        private InputManager Input { get; set; }
        private Vector3 MoveDir => Input.Player.Move.ReadValue<Vector3>();
        
        [field: SerializeField] private PlayerAttributeData AttributeData { get; set; }
        [field: SerializeField] private Rigidbody Rigidbody { get; set; }
        
        private void Awake()
        {
            Input = InputSystem.Input;

            var info = $"\n遊戲物件：{gameObject.name}\n遊戲組件：{GetType().Name}\n";
            if (Tools.CheckNull(Input is null, $"找尋不到 InputSystem 組件！{info}") ||
                Tools.CheckNull(!AttributeData, $"AttributeData 未被掛載！{info}") ||
                Tools.CheckNull(!Rigidbody, $"Rigidbody 未被掛載！{info}")) enabled = false;
        }

        private void FixedUpdate()
        {
            if (MoveDir == Vector3.zero) return;
            Rigidbody.linearVelocity = transform.InverseTransformDirection(MoveDir) * (AttributeData.MoveSpeed * Time.fixedDeltaTime);
        }
    }
}