using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace System.Economy.Child.Customer.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private NavMeshAgent Agent;
        
        [field: Header("Point")]
        [field: SerializeField] private Transform Root;
        [field: SerializeField] private Transform Hip;

        private IEnumerator WalkCor;
        
        private bool isLeft;

        public event Action ToLeft;
        public event Action ToRight;

        private void Awake()
        {
            Agent.updateRotation = false;
        }

        private void OnDisable()
        {
            StopWalk();
        }

        public void StartWalk(Transform target, Action onArrive = null)
        {
            Agent.enabled = true;
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
                            ToRight?.Invoke();
                            break;
                        }
                        case < 0 when isLeft:
                        {
                            isLeft = false;
                            ToLeft?.Invoke();
                            break;
                        }
                    }
                    
                    if (Agent.pathPending)                                    { yield return null; continue; }
                    if (float.IsInfinity(Agent.remainingDistance))            { yield return null; continue; }
                    if (Agent.remainingDistance >= .05f)                      { yield return null; continue; }
                    if (!Agent.hasPath || Agent.velocity.sqrMagnitude < .01f) { yield return null; break; }
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

        public void SitDown(Transform sitPoint)
        {
            var direction = sitPoint.position - Hip.position;
            Root.transform.position += direction;

            Agent.ResetPath();
            Agent.enabled = false;
        }
    }
}