using System;
using System.Collections;
using Common.Data.Player.Child.Player_Attribute;
using Input_System.Main;
using UnityEngine;

namespace Player_System.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Rigidbody Rig;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerAttributeSO playerAttributeSO;

        private IEnumerator WalkCor;

        private bool isLeft = true;

        public event Action WalkLeft;
        public event Action WalkRight;

        private void OnDisable()
        {
            StopWalk();
        }

        public void StartWalk()
        {
            WalkCor = WalkCoroutine();
            StartCoroutine(WalkCor);
            return;

            IEnumerator WalkCoroutine()
            {
                while (true)
                {
                    InputSystem.GetPlayerWalkDirection(out var direction);
                    playerAttributeSO.GetMoveSpeed(out var speed);
                    
                    Rig.linearVelocity = new Vector3(
                        x: direction.x * (speed * Time.fixedDeltaTime),
                        y: Rig.linearVelocity.y,
                        z: direction.y * (speed * Time.fixedDeltaTime));
                    
                    switch (Rig.linearVelocity.x)
                    {
                        case > 0 when isLeft:
                        {
                            isLeft = false;
                            WalkRight?.Invoke();
                            break;
                        }
                        case < 0 when !isLeft:
                        {
                            isLeft = true;
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
            if (WalkCor is null) return;
            Rig.linearVelocity = new Vector3(0, Rig.linearVelocity.y, 0);
            StopCoroutine(WalkCor);
            WalkCor = null;
        }
    }
}