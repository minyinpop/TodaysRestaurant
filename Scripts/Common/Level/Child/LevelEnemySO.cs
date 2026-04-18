using Common.Enemy_Battle_Group;
using Common.Enemy_Data;
using Common.Value;
using UnityEngine;

namespace Common.Level.Child
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
        [field: Header("Explore System")]
        [field: SerializeField] private ExploreEnemyEntry exploreEnemyEntry;
                                public ExploreEnemyEntry ExploreEnemyEntry => exploreEnemyEntry;
        
        [field: Header("Battle System")]
        [field: SerializeField] private EnemyBattleGroupSO enemyBattleGroupData;
                                public EnemyBattleGroupSO EnemyBattleGroupData => enemyBattleGroupData;
    }

    [System.Serializable]
    public sealed class ExploreEnemyEntry
    {
        [field: SerializeField] private EnemySO enemyData;
                                public EnemySO EnemyData => enemyData;
        [field: SerializeField] private Range spawnAmount;
                                public Range SpawnAmount => spawnAmount;
    }
}