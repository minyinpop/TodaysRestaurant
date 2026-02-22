using Common.Item.Data.Ingredient;
using Common.Value;
using UnityEngine;

namespace Common.Level.Child.Level_Possible_Items
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Child/Level Ingredient", fileName = "New Data")]
    public sealed class LevelIngredient : ScriptableObject
    {
        [field: Header("Data")]
        [field: SerializeField] private LevelIngredientEntry[] levelIngredientEntries;
                                public LevelIngredientEntry[] LevelIngredientEntries => levelIngredientEntries;

        private void OnValidate()
        {
            foreach (var entry in levelIngredientEntries)
            {
                if (entry.IngredientData == null)
                {
                    Debug.Log($"{GetType().Name} > {nameof(entry.IngredientData)} cannot be null.");
                }

                if (entry.SpawnAmount.Min < 0)
                {
                    Debug.Log($"{GetType().Name} > {nameof(entry.SpawnAmount.Min)} cannot be negative.");
                }

                if (entry.SpawnAmount.Max < 0)
                {
                    Debug.Log($"{GetType().Name} > {nameof(entry.SpawnAmount.Max)} cannot be negative.");
                }
            }
        }
    }

    [System.Serializable]
    public sealed class LevelIngredientEntry
    {
        [field: SerializeField] private IngredientSO ingredientData;
                                public IngredientSO IngredientData => ingredientData;
        [field: SerializeField] private Range spawnAmount;
                                public Range SpawnAmount => spawnAmount;
    }
}