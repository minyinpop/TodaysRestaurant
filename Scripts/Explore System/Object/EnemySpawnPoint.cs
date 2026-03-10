using Common.Enemy.Enemy_Object;
using UnityEngine;
using UnityEngine.AI;

namespace Explore_System.Object
{
    public sealed class EnemySpawnPoint : MonoBehaviour
    {
        private EnemyObject _currentEnemyObject;

        public void InitializeEnemy(EnemyObject enemyObject)
        {
            if (enemyObject is null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(InitializeEnemy)} > {nameof(enemyObject)} cannot be null.");
                return;
            }

            if (_currentEnemyObject is not null)
            {
                Debug.Log($"{gameObject.name} is already occupied.");
                return;
            }

            if (NavMesh.SamplePosition(transform.position, out var hit, float.MaxValue, NavMesh.AllAreas))
            {
                var prefab = enemyObject.gameObject;
                var position = hit.position;
                var rotation = enemyObject.transform.rotation;
                var parent = transform;
                
                _currentEnemyObject = Instantiate(prefab, position, rotation, parent).GetComponent<EnemyObject>();
                _currentEnemyObject.Initialize(hit.position);
            }
            else
            {
                Debug.Log($"{name} > {GetType().Name} cannot found a valid position.");
            }

        }
    }
}