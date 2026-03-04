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
            var _remainingEnemyEntry = levelData.LevelEnemy.EnemiesData.ToList();
        }
    }
}