using System.Linq;
using Common.Level.Main;
using Explore_System.Object;
using UnityEngine;

namespace Explore_System.System.Child
{
    public sealed class IngredientSystem : MonoBehaviour
    {
        [field: Header("Ingredient Settings")]
        [field: SerializeField] private IngredientSpawnPoint[] ingredientSpawnPoints;
        
        private LevelSO _levelData;

        private bool _initialized;

        public void InitializeIngredient(LevelSO levelData)
        {
            _levelData = levelData;
            
            if (_initialized)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_initialized)} is already initialize.");
                Destroy(gameObject);
                return;
            }

            var _remainingIngredientEntry = _levelData.LevelIngredient.LevelIngredientEntries.ToList();
            var _remainingSpawnPoints = ingredientSpawnPoints.ToList();

            while (_remainingIngredientEntry.Count > 0 && _remainingSpawnPoints.Count > 0)
            {
                var entry = _remainingIngredientEntry[Random.Range(0, _remainingIngredientEntry.Count)];
                _remainingIngredientEntry.Remove(entry);
                var spawnAmount = Random.Range(entry.SpawnAmount.Min, entry.SpawnAmount.Max + 1);

                if (spawnAmount <= 0)
                {
                    continue;
                }

                for (var i = 0; i < spawnAmount; i++)
                {
                    if (_remainingSpawnPoints.Count <= 0)
                    {
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