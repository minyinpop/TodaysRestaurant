using Common.Enemy;
using Common.Enemy.Data;
using UnityEngine;

namespace Common.Level.Child.Level_Possible_Enemies
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Child/Level Enemy", fileName = "New Data")]
    public sealed class LevelEnemy : ScriptableObject
    {
        [field: Header("Data")]
        [field: SerializeField] private EnemySO[] enemiesData;
                                public EnemySO[] EnemiesData => enemiesData;
    }
}