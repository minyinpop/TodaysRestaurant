using UnityEngine;

namespace Common.Enemy_Explore_Object
{
    public partial class EnemyExploreObject
    {
        [field: Header("Move Settings")]
        [field: SerializeField] private float moveSpeed = 1f;

        private void StartMove()
        {
            _agent.isStopped = false;
            _agent.speed = moveSpeed;
                
            _agent.updateRotation = false;
            
            _agent.SetDestination(_chaseTarget is null ? _lastChaseTargetPosition : _chaseTarget.transform.position);
        }

        private void StopMove()
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }
    }
}