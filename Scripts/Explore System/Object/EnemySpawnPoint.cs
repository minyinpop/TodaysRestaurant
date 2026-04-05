using System;
using Common.Enemy_Battle_Group;
using Common.Enemy.Enemy_Object;
using Common.Level.Child;
using UnityEngine;
using UnityEngine.AI;

namespace Explore_System.Object
{
    public sealed class EnemySpawnPoint : MonoBehaviour
    {
        private EnemyObject _currentEnemyObject;

        public void InitializeEnemy(EnemyObject enemyObject, EnemyBattleGroupSO enemyBattleGroupData)
        {
            if (enemyObject is null)
            {
                throw new ArgumentException(nameof(enemyObject));
            }

            if (_currentEnemyObject is not null)
            {
                throw new InvalidOperationException($"{nameof(_currentEnemyObject)} is not empty.");
            }

            if (NavMesh.SamplePosition(transform.position, out var hit, float.MaxValue, NavMesh.AllAreas))
            {
                var prefab = enemyObject.gameObject;
                var position = hit.position;
                var rotation = enemyObject.transform.rotation;
                var parent = transform;
                
                _currentEnemyObject = Instantiate(prefab, position, rotation, parent).GetComponent<EnemyObject>();
                _currentEnemyObject.Initialize(hit.position, enemyBattleGroupData);
            }
            else
            {
                Debug.Log($"{name} > {GetType().Name} cannot found a valid position.");
                Destroy(gameObject);
            }
        }
    }
}