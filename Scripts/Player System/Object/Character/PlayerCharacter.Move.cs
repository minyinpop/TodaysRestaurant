using Common.Player.Child.Player_Attribute;
using Input_System;
using UnityEngine;

namespace Player_System.Object.Character
{
    public partial class PlayerCharacter
    {
        [field: Header("Move System")]
        [field: SerializeField] private Rigidbody rig;
        [field: SerializeField] private PlayerAttributeSO attributeSO;

        private bool _isWalking;
        
        private void DetectMove()
        {
            if (rig.linearVelocity == Vector3.zero && _isWalking)
            {
                _isWalking = false;
                IdleState();
            }
            else if (rig.linearVelocity != Vector3.zero && !_isWalking)
            {
                _isWalking = true;
                WalkState();
            }
            
            var direction = InputSystem.WalkDirection;
            attributeSO.GetMoveSpeed(out var speed);
            
            rig.linearVelocity = new Vector3(
                x: direction.x * (speed * Time.fixedDeltaTime),
                y: rig.linearVelocity.y,
                z: direction.y * (speed * Time.fixedDeltaTime));
        }

        private void DetectFlip()
        {
            if (rig.linearVelocity.x > 0)
            {
                TurnsRight();
            }
            else if (rig.linearVelocity.x < 0)
            {
                TurnsLeft();
            }
        }
    }
}