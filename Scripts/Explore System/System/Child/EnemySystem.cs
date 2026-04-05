using System.Linq;
using Common.Level.Main;
using Explore_System.Object;
using UnityEngine;

namespace Explore_System.System.Child
{
    public sealed class EnemySystem : MonoBehaviour
    {
        [field: Header("Enemy Settings")]
        [field: SerializeField] private EnemySpawnPoint[] enemySpawnPoints;
        
        private LevelSO _levelData;
        
        private bool _initialized;

        public void InitializeEnemy(LevelSO levelData)
        {
            _levelData = levelData;
            
            if (_initialized)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_initialized)} is already initialize.");
                Destroy(gameObject);
                return;
            }
            
            var _remainingEnemyEntry = _levelData.LevelEnemyData.EnemyEntries.ToList();
            var _remainingSpawnPoints = enemySpawnPoints.ToList();

            while (_remainingEnemyEntry.Count > 0 && _remainingSpawnPoints.Count > 0)
            {
                var entry = _remainingEnemyEntry[Random.Range(0, _remainingEnemyEntry.Count)];
                            _remainingEnemyEntry.Remove(entry);
                var spawnAmount = Random.Range(entry.ExploreEnemyEntry.SpawnAmount.Min, entry.ExploreEnemyEntry.SpawnAmount.Max + 1);
                
                if (spawnAmount <= 0)
                {
                    continue;
                }

                for (var i = 0; i < spawnAmount; i ++)
                {
                    if (_remainingSpawnPoints.Count <= 0)
                    {
                        break;
                    }

                    var spawnPoint = _remainingSpawnPoints[Random.Range(0, _remainingSpawnPoints.Count)];
                                     _remainingSpawnPoints.Remove(spawnPoint);
                    spawnPoint.InitializeEnemy(entry.ExploreEnemyEntry.EnemyData.EnemyObject, entry.EnemyBattleGroupData);
                }
            }
        }
    }
}