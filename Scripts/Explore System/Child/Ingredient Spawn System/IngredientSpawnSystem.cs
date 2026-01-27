using Explore_System.Child.Ingredient_Spawn_System.Data;
using UnityEngine;

namespace Explore_System.Child.Ingredient_Spawn_System
{
    public sealed class IngredientSpawnSystem : MonoBehaviour
    {
        [field: Header("Transform")]
        [field: SerializeField] private Transform[] spawnPoints;
        
        [field: Header("Data")]
        [field: SerializeField] private IngredientSpawnData ingredientSpawnData;

        public void SpawnIngredient()
        {
        }
    }
}