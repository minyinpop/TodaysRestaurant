using System;
using Common.Enemy_Battle_Group;
using Common.Enemy_Explore_Object;
using UnityEngine;
using UnityEngine.AI;

namespace Explore_System.Object
{
    public sealed class EnemySpawnPoint : MonoBehaviour
    {
        public EnemyExploreObject EnemyExploreObject { get; private set; }

        public void InitializeEnemy(EnemyExploreObject enemyExploreObject, EnemyBattleGroupSO enemyBattleGroupData)
        {
            if (enemyExploreObject is null)
            {
                throw new ArgumentException(nameof(enemyExploreObject));
            }

            if (this.EnemyExploreObject is not null)
            {
                throw new InvalidOperationException($"{nameof(this.EnemyExploreObject)} is not empty.");
            }

            if (NavMesh.SamplePosition(transform.position, out var hit, float.MaxValue, NavMesh.AllAreas))
            {
                var prefab = enemyExploreObject.gameObject;
                var position = hit.position;
                var rotation = enemyExploreObject.transform.rotation;
                var parent = transform;
                
                this.EnemyExploreObject = Instantiate(prefab, position, rotation, parent).GetComponent<EnemyExploreObject>();
                this.EnemyExploreObject.Initialize(hit.position, enemyBattleGroupData);
            }
            else
            {
                Debug.Log($"{name} > {GetType().Name} cannot found a valid position.");
                Destroy(gameObject);
            }
        }
    }
}