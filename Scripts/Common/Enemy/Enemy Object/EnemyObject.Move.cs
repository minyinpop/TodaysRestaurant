using UnityEngine;
using UnityEngine.AI;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Move Settings")]
        [field: SerializeField] private NavMeshAgent navMeshAgent;

        private void ChasingTarget()
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = 1;
            
            navMeshAgent.updateRotation = false;
            
            navMeshAgent.SetDestination(_chasingTarget.transform.position);
        }
    }
}