using Input_System;
using Player_System.Data.Child.Player_Attribute;
using UnityEngine;

namespace Player_System.Object
{
    public partial class PlayerObject
    {
        [field: Header("Move System")]
        [field: SerializeField] private Rigidbody rig;
        [field: SerializeField] private PlayerAttributeSO attributeData;

        private bool _canWalk = true;
        private bool _isWalking;
        
        private void DetectMove()
        {
            if (!_canWalk)
            {
                _isWalking = false;
                
                rig.linearVelocity = Vector3.zero;
                return;
            }

            var direction = InputSystem.WalkDirection;
            rig.linearVelocity = new Vector3(
                x: direction.x * (attributeData.MoveSpeed * Time.fixedDeltaTime),
                y: rig.linearVelocity.y,
                z: direction.y * (attributeData.MoveSpeed * Time.fixedDeltaTime));
            
            if (direction == Vector2.zero && _isWalking)
            {
                _isWalking = false;
                _stateMachine.ChangeState(_idleState);
            }
            else if (direction != Vector2.zero && !_isWalking)
            {
                _isWalking = true;
                _stateMachine.ChangeState(_moveState);
            }

            if (direction != Vector2.zero)
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
}