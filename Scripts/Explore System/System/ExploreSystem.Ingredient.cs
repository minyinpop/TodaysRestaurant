using System.Linq;
using Explore_System.Object;
using UnityEngine;

namespace Explore_System.System
{
    public partial class ExploreSystem
    {
        [field: Header("Ingredient Settings")]
        [field: SerializeField] private IngredientSpawnPoint[] ingredientSpawnPoints;

        private void InitializeIngredient()
        {
            var _remainingIngredientEntry = _levelData.LevelIngredient.LevelIngredientEntries.ToList();
            var _remainingSpawnPoints = ingredientSpawnPoints.ToList();

            while (_remainingIngredientEntry.Count > 0 && _remainingSpawnPoints.Count > 0)
            {
                var entry = _remainingIngredientEntry[Random.Range(0, _remainingIngredientEntry.Count)];
                            _remainingIngredientEntry.Remove(entry);
                var spawnAmount = Random.Range(entry.SpawnAmount.Min, entry.SpawnAmount.Max + 1);
                
                if (spawnAmount <= 0)
                {
                    Debug.Log($"{entry.IngredientData.ItemName} 的生成數量等於 0，嘗試換到下一個。"); // TODO 開發專用，記得刪除
                    continue;
                }

                for (var i = 0; i < spawnAmount; i ++)
                {
                    if (_remainingSpawnPoints.Count <= 0)
                    {
                        Debug.Log("沒有足夠的 " + nameof(IngredientSpawnPoint) + " 可供生成了。"); // TODO 開發專用，記得刪除
                        break;
                    }

                    var spawnPoint = _remainingSpawnPoints[Random.Range(0, _remainingSpawnPoints.Count)];
                                     _remainingSpawnPoints.Remove(spawnPoint);
                    spawnPoint.InitializeIngredient(entry.IngredientData.ItemObject);
                }
            }
        }
    }
}