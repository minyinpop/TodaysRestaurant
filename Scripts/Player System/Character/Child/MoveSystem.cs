using System;
using System.Collections;
using Input_System.Main;
using Player_System.Data.Main;
using UnityEngine;

namespace Player_System.Character.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Rigidbody Rig;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private IEnumerator WalkCor;

        private bool isLeft;

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
                    PlayerData.GetMoveSpeed(out var speed);
                    
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