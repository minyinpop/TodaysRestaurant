using System.Collections;
using System.Input.Main;
using Data.Player.Main;
using UnityEngine;

namespace System.Player.Character.Child
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
            StopCoroutine(WalkCor);
            WalkCor = null;
        }
    }
}