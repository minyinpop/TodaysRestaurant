using System.Collections.Generic;
using System.Linq;
using Explore_System.Object;
using UnityEngine;

namespace Explore_System.System
{
    public partial class ExploreSystem
    {
        [field: SerializeField] private ResourceSpawnPoint[] resourceSpawnPoints;
        
        private readonly List<ResourceSpawnPoint> _remainingAvailableResourceSpawnPoints = new();
        
        private void InitializeResource()
        {
            _remainingAvailableResourceSpawnPoints.AddRange(resourceSpawnPoints);

            var rename = levelData.LevelIngredient.IngredientsData.ToList();

            for (var i = rename.Count - 1; i >= 0; i--)
            {
                var ingredientData = rename[Random.Range(0, rename.Count)];
                Debug.Log(ingredientData.ItemName);
                rename.Remove(ingredientData);
                // TODO 要做一個 SO 專門讀取這個 IngredientData 可以生成多少個，min to max range
            }
        }
    }
}