using System.Collections.Generic;
using Explore_System.Object;
using UnityEngine;

namespace Explore_System.System
{
    public partial class ExploreSystem
    {
        [field: SerializeField] private ResourceSpawnPoint[] resourceSpawnPoints;
        
        private readonly List<ResourceSpawnPoint> _remainingSpawnPoints = new();
        
        private void InitializeResource()
        {
            _remainingSpawnPoints.AddRange(resourceSpawnPoints);

            foreach (var entry in levelData.LevelIngredient.LevelIngredientEntries)
            {
                var spawnAmount = Random.Range(entry.SpawnAmount.Min, entry.SpawnAmount.Max);

                if (spawnAmount <= 0)
                {
                    continue;
                }
                
                // 決定 entry 裡的 IngredientData 要在哪幾個 _remainingSpawnPoints 生成
            }
        }
    }
}