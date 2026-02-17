using System;
using System.Collections;
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

        private IEnumerator _walkCoroutine;

        private bool _isLeft = true;

        public event Action WalkLeft;
        public event Action WalkRight;

        private void OnDisable()
        {
            StopWalk();
        }

        public void StartWalk()
        {
            _walkCoroutine = WalkCoroutine();
            StartCoroutine(_walkCoroutine);
            return;

            IEnumerator WalkCoroutine()
            {
                while (true)
                {
                    var direction = InputSystem.WalkDirection;
                    attributeSO.GetMoveSpeed(out var speed);
                    
                    rig.linearVelocity = new Vector3(
                        x: direction.x * (speed * Time.fixedDeltaTime),
                        y: rig.linearVelocity.y,
                        z: direction.y * (speed * Time.fixedDeltaTime));
                    
                    switch (rig.linearVelocity.x)
                    {
                        case > 0 when _isLeft:
                        {
                            _isLeft = false;
                            WalkRight?.Invoke();
                            break;
                        }
                        case < 0 when !_isLeft:
                        {
                            _isLeft = true;
                            WalkLeft?.Invoke();
                            break;
                        }
                    }
                    
                    yield return new WaitForFixedUpdate();
                }
            }
        }

        public void StopWalk()
        {
            if (_walkCoroutine is null) return;
            rig.linearVelocity = new Vector3(0, rig.linearVelocity.y, 0);
            StopCoroutine(_walkCoroutine);
            _walkCoroutine = null;
        }
    }
}