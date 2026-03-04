using Common.Enemy.Data;
using Common.Value;
using UnityEngine;

namespace Common.Level.Child.Level_Possible_Enemies
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Child/Level Enemy", fileName = "New Data")]
    public sealed class LevelEnemy : ScriptableObject
    {
        [field: Header("Data")]
        [field: SerializeField] private LevelEnemyEntry[] levelEnemyEntries;
                                public LevelEnemyEntry[] LevelEnemyEntries => levelEnemyEntries;

        private void OnValidate()
        {
            foreach (var entry in levelEnemyEntries)
            {
                if (entry.EnemyData is null)
                {
                    Debug.Log($"{GetType().Name} > {nameof(entry.EnemyData)} cannot be null.");
                }

                if (entry.SpawnAmount.Min < 0)
                {
                    Debug.Log($"{GetType().Name} > {nameof(entry.SpawnAmount.Min)} cannot be negative.");
                }

                if (entry.SpawnAmount.Max < 0)
                {
                    Debug.Log($"{GetType().Name} > {nameof(entry.SpawnAmount.Max)} cannot be negative.");
                }

                if (entry.SpawnAmount.Max < entry.SpawnAmount.Min)
                {
                    Debug.Log($"{nameof(entry.SpawnAmount.Max)} cannot be less than {nameof(entry.SpawnAmount.Min)}.");
                }
            }
        }
    }

    [System.Serializable]
    public sealed class LevelEnemyEntry
    {
        [field: SerializeField] private EnemySO enemyData;
                                public EnemySO EnemyData => enemyData;
        [field: SerializeField] private Range spawnAmount;
                                public Range SpawnAmount => spawnAmount;
    }
}