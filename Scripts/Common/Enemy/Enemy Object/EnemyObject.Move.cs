using UnityEngine;
using UnityEngine.AI;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Move Settings")]
        [field: SerializeField] private NavMeshAgent navMeshAgent;
        [field: SerializeField] private float moveSpeed = 1f;

        private bool _isStartMoved;

        private void StartMove()
        {
            if (_isStartMoved)
            {
                Debug.Log($"{nameof(EnemyObject)} > {nameof(StartMove)} is already set.");
                return;
            }
            
            _isStartMoved = true;

            navMeshAgent.isStopped = false;
            navMeshAgent.speed = moveSpeed;
                
            navMeshAgent.updateRotation = false;
            
            navMeshAgent.SetDestination(_chasingTarget.transform.position);
        }

        private void KeepMoving()
        {
            if (!_isStartMoved)
            {
                Debug.Log($"{nameof(EnemyObject)} > {nameof(StartMove)} is not start move yet.)");
                return;
            }

            navMeshAgent.SetDestination(_chasingTarget.transform.position);
        }

        private void StopMove()
        {
            if (!_isStartMoved)
            {
                Debug.Log($"{nameof(EnemyObject)} > {nameof(StartMove)} is not start move yet.)");
                return;
            }
            
            _isStartMoved = false;

            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
            
            navMeshAgent.updateRotation = false;

            navMeshAgent.SetDestination(Vector3.zero);
        }
    }
}