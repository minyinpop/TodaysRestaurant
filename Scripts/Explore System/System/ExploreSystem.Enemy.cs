using System.Linq;
using Explore_System.Object;
using UnityEngine;

namespace Explore_System.System
{
    public partial class ExploreSystem
    {
        [field: Header("Enemy Settings")]
        [field: SerializeField] private EnemySpawnPoint[] enemySpawnPoints;

        private void InitializeEnemy()
        {
            var _remainingEnemyEntry = levelData.LevelEnemy.LevelEnemyEntries.ToList();
            var _remainingSpawnPoints = enemySpawnPoints.ToList();

            for (var i = 0; i <= _remainingEnemyEntry.Count; i++)
            {
                var entry = _remainingEnemyEntry[Random.Range(0, _remainingEnemyEntry.Count)];
                                    _remainingEnemyEntry.Remove(entry);
                var spawnAmount = Random.Range(entry.SpawnAmount.Min, entry.SpawnAmount.Max);

                if (spawnAmount <= 0)
                {
                    Debug.Log($"{entry.EnemyData.name} 的生成數量低於 0，所以重新抽一個"); // TODO Develop Only
                    continue;
                }

                for (var ii = spawnAmount; ii > 0; ii--)
                {
                    if (_remainingSpawnPoints.Count <= 0)
                    {
                        Debug.Log("沒有足夠的資源生成點可供資源生成了！"); // TODO Develop Only
                        break;
                    }

                    var spawnPoint = _remainingSpawnPoints[Random.Range(0, _remainingSpawnPoints.Count)];
                                     _remainingSpawnPoints.Remove(spawnPoint);
                                     spawnPoint.InitializeEnemy(entry.EnemyData.EnemyObject);
                }
            }
        }
    }
}