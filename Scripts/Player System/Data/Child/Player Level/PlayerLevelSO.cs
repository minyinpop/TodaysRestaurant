using Common.Enemy.Data;
using Common.Item.Data;
using UnityEngine;

namespace Player_System.Data.Child.Player_Level
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Level", fileName = "New Data")]
    public sealed class PlayerLevelSO : ScriptableObject
    {
        [field: SerializeField] private UnlockLevel[] unlockLevels;
    }

    [global::System.Serializable]
    public sealed class UnlockLevel
    {
        [field: SerializeField] private UnlockItem[] unlockItems;
        [field: SerializeField] private UnlockEnemy[] unlockEnemies;
    }

    [global::System.Serializable]
    public sealed class UnlockItem
    {
        [field: SerializeField] private ItemSO item;
        [field: SerializeField] private bool isUnlock;
    }

    [global::System.Serializable]
    public sealed class UnlockEnemy
    {
        [field: SerializeField] private EnemySO enemy;
        [field: SerializeField] private bool isUnlock;
    }
}