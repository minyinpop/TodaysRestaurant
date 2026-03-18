using Common.Enemy.Data;
using Common.Item.Data;
using Common.Value;
using UnityEngine;

namespace Common.Level.Child.Level_Enemy
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Child/Level Enemy", fileName = "New Data")]
    public sealed class LevelEnemySO : ScriptableObject
    {
        [field: SerializeField] private LevelEnemyEntry[] enemyEntries;
                                public LevelEnemyEntry[] EnemyEntries => enemyEntries;
    }

    [System.Serializable]
    public sealed class LevelEnemyEntry
    {
        [field: SerializeField] private ExploreEnemyEntry exploreEnemyEntry;
                                public ExploreEnemyEntry ExploreEnemyEntry => exploreEnemyEntry;
        [field: SerializeField] private BattleEnemyEntry battleEnemyEntry;
                                public BattleEnemyEntry BattleEnemyEntry => battleEnemyEntry;
    }

    [System.Serializable]
    public sealed class ExploreEnemyEntry
    {
        [field: SerializeField] private EnemySO enemyData;
                                public EnemySO EnemyData => enemyData;
        [field: SerializeField] private Range spawnAmount;
                                public Range SpawnAmount => spawnAmount;
    }

    [System.Serializable]
    public sealed class BattleEnemyEntry
    {
        [field: SerializeField] private EnemySO[] enemiesData;
                                public EnemySO[] EnemiesData => enemiesData;
        [field: SerializeField] private ItemSO[] itemsData;
                                public ItemSO[] ItemsData => itemsData;
    }
}