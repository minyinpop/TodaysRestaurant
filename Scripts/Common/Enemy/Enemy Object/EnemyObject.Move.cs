using UnityEngine;
using UnityEngine.AI;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Move Settings")]
        [field: SerializeField] private NavMeshAgent navMeshAgent;
        [field: SerializeField] private float moveSpeed = 1f;

        private void StartMove()
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = moveSpeed;
                
            navMeshAgent.updateRotation = false;
            
            navMeshAgent.SetDestination(_chaseTarget is null ? _lastChaseTargetPosition : _chaseTarget.transform.position);
        }

        private void StopMove()
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.ResetPath();
        }
    }
}