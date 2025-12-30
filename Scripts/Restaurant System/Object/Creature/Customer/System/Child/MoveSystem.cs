using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Restaurant_System.Object.Creature.Customer.System.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private NavMeshAgent agent;
        
        [field: Header("Point")]
        [field: SerializeField] private Transform root;
        [field: SerializeField] private Transform hip;

        private IEnumerator _walkCor;
        
        private bool _isLeft;

        public event Action ToLeft;
        public event Action ToRight;

        private void Awake()
        {
            agent.updateRotation = false;
        }

        private void OnDisable()
        {
            StopWalk();
        }

        #region Walk
            public void StartWalk(Transform target, Action onArrive = null)
            {
                agent.enabled = true;
                agent.SetDestination(target.position);
                
                _walkCor = WalkCoroutine();
                StartCoroutine(_walkCor);
                return;

                IEnumerator WalkCoroutine()
                {
                    while (true)
                    {
                        var moveDir = agent.velocity.sqrMagnitude > .0001f ? agent.velocity.normalized : target.forward;
                        switch (moveDir.x)
                        {
                            case > 0 when !_isLeft:
                            {
                                _isLeft = true;
                                ToRight?.Invoke();
                                break;
                            }
                            case < 0 when _isLeft:
                            {
                                _isLeft = false;
                                ToLeft?.Invoke();
                                break;
                            }
                        }
                        
                        if (agent.pathPending)                                    { yield return null; continue; }
                        if (float.IsInfinity(agent.remainingDistance))            { yield return null; continue; }
                        if (agent.remainingDistance >= .05f)                      { yield return null; continue; }
                        if (!agent.hasPath || agent.velocity.sqrMagnitude < .01f) { yield return null; break; }
                        yield return null;
                    }

                    onArrive?.Invoke();
                }
            }
            
            public void StopWalk()
            {
                if (_walkCor is null) return;
                StopCoroutine(_walkCor);
                _walkCor = null;
            }
        #endregion

        #region Chair
            public void SitDown(Transform sitPoint)
            {
                var direction = sitPoint.position - hip.position;
                root.transform.position += direction;
                
                if (agent is null) return;
                if (!agent.isActiveAndEnabled) return;
                if (!agent.isOnNavMesh) return;

                agent.ResetPath();
                agent.enabled = false;
            }

            public void StandUp(Transform standPoint)
            {
                root.transform.position = standPoint.position;

                if (agent is null) return;
                if (!agent.isActiveAndEnabled) return;
                if (!agent.isOnNavMesh) return;
                
                agent.ResetPath();
                agent.enabled = true;
            }
        #endregion
    }
}