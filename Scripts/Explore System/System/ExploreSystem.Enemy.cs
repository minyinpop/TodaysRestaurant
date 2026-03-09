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
            var _remainingEnemyEntry = _levelData.LevelEnemy.LevelEnemyEntries.ToList();
            var _remainingSpawnPoints = enemySpawnPoints.ToList();

            while (_remainingEnemyEntry.Count > 0 && _remainingSpawnPoints.Count > 0)
            {
                var entry = _remainingEnemyEntry[Random.Range(0, _remainingEnemyEntry.Count)];
                            _remainingEnemyEntry.Remove(entry);
                var spawnAmount = Random.Range(entry.SpawnAmount.Min, entry.SpawnAmount.Max + 1);
                
                if (spawnAmount <= 0)
                {
                    Debug.Log($"{entry.EnemyData.name} 的生成數量等於 0，嘗試換到下一個。"); // TODO 開發專用，記得刪除
                    continue;
                }

                for (var i = 0; i < spawnAmount; i ++)
                {
                    if (_remainingSpawnPoints.Count <= 0)
                    {
                        Debug.Log("沒有足夠的 " + nameof(EnemySpawnPoint) + " 可供生成了。"); // TODO 開發專用，記得刪除
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