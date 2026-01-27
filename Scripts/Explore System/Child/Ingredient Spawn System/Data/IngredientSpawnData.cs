using Common.Value;
using UnityEngine;

namespace Explore_System.Child.Ingredient_Spawn_System.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Explore System/Ingredient Spawn Data", fileName = "New Data")]
    public sealed class IngredientSpawnData : ScriptableObject
    {
        [field: Header("Spawn Entry")]
        [field: SerializeField] private IngredientSpawnEntry[] ingredientSpawnEntries;
                                public IngredientSpawnEntry[] IngredientSpawnEntries => ingredientSpawnEntries;
    }
    
    [System.Serializable]
    public sealed class IngredientSpawnEntry
    {
        [field: Header("Prefab")]
        [field: SerializeField] private GameObject ingredientPrefab;
                                public GameObject IngredientPrefab => ingredientPrefab;
                                
        [field: Header("Spawn Amount")]
        [field: SerializeField] private Range spawnAmount;
                                public Range SpawnAmount => spawnAmount;
    }
}