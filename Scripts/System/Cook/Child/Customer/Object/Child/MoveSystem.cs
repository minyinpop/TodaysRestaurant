using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace System.Cook.Child.Customer.Object.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private NavMeshAgent Agent;

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

        public void StartWalk(Transform target, Action onArrive)
        {
            Agent.SetDestination(target.position);
            
            WalkCor = WalkCoroutine();
            StartCoroutine(WalkCor);
            return;

            IEnumerator WalkCoroutine()
            {
                while (true)
                {
                    var moveDir = Agent.velocity.sqrMagnitude > .0001f ? Agent.velocity.normalized : target.forward;
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
                    
                    if (Agent.pathPending)                         { yield return null; continue; }
                    if (float.IsInfinity(Agent.remainingDistance)) { yield return null; continue; }
                    if (Agent.remainingDistance < .0001f)          { yield return null; break; }
                    yield return null;
                }

                onArrive?.Invoke();
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