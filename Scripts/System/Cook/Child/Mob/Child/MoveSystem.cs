using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace System.Cook.Child.Mob.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private NavMeshAgent Agent;
        [field: SerializeField] private Transform Target;

        private IEnumerator WalkCor;


        private bool isLeft;

        public event Action WalkLeft;
        public event Action WalkRight;

        private void Awake()
        {
            Agent.updateRotation = false;
        }

        private void OnDisable()
        {
            StopWalk();
        }

        public void StartWalk()
        {
            Agent.SetDestination(Target.position);
            
            WalkCor = WalkCoroutine();
            StartCoroutine(WalkCor);
            return;

            IEnumerator WalkCoroutine()
            {
                while (true)
                {
                    var moveDir = Agent.velocity.sqrMagnitude > .0001f ? Agent.velocity.normalized : Target.forward;
                    Debug.Log(moveDir);
                    switch (moveDir.x)
                    {
                        case > 0 when !isLeft:
                        {
                            isLeft = true;
                            WalkRight?.Invoke();
                            break;
                        }
                        case < 0 when isLeft:
                        {
                            isLeft = false;
                            WalkLeft?.Invoke();
                            break;
                        }
                    }
                    
                    yield return null;
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