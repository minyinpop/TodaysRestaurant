using System;
using Common.Enemy_Battle_Group;
using Common.Enemy.Enemy_Object;
using UnityEngine;
using UnityEngine.AI;

namespace Explore_System.Object
{
    public sealed class EnemySpawnPoint : MonoBehaviour
    {
        public EnemyObject enemyObject { get; private set; }

        public void InitializeEnemy(EnemyObject enemyObject, EnemyBattleGroupSO enemyBattleGroupData)
        {
            if (enemyObject is null)
            {
                throw new ArgumentException(nameof(enemyObject));
            }

            if (this.enemyObject is not null)
            {
                throw new InvalidOperationException($"{nameof(this.enemyObject)} is not empty.");
            }

            if (NavMesh.SamplePosition(transform.position, out var hit, float.MaxValue, NavMesh.AllAreas))
            {
                var prefab = enemyObject.gameObject;
                var position = hit.position;
                var rotation = enemyObject.transform.rotation;
                var parent = transform;
                
                this.enemyObject = Instantiate(prefab, position, rotation, parent).GetComponent<EnemyObject>();
                this.enemyObject.Initialize(hit.position, enemyBattleGroupData);
            }
            else
            {
                Debug.Log($"{name} > {GetType().Name} cannot found a valid position.");
                Destroy(gameObject);
            }
        }
    }
}