using System;
using Player_Attribute;
using UnityEngine;

namespace Player.Base
{
    internal class PlayerMovement : MonoBehaviour
    {
        private InputManager Input { get; set; }
        private Vector3 MoveDirection => Input.Player.Move.ReadValue<Vector3>();
        
        [field: SerializeField] private PlayerAttributeData PlayerAttributeData { get; set; }
        [field: SerializeField] private Rigidbody Rigidbody { get; set; }
        
        private void Awake() => Input = InputSystem.Input;

        private void FixedUpdate()
        {
            if (MoveDirection == Vector3.zero) return;
            var direction = transform.InverseTransformDirection(MoveDirection);
            var speed = PlayerAttributeData.MoveSettings.MoveSpeed * Time.fixedDeltaTime;
            Rigidbody.linearVelocity = direction * speed;
        }
    }
}