using System.Collections;
using System.Input.Main;
using Data.Player.Base;
using UnityEngine;

namespace System.Player.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Rigidbody Rig;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private IEnumerator WalkCor;

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