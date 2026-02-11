using Common.Data.Enemy;
using Common.Data.Item;
using UnityEngine;

namespace Common.Data.Player.Child.Player_Level
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Level", fileName = "New Data")]
    public sealed class PlayerLevelSO : ScriptableObject
    {
        [field: SerializeField] private UnlockLevel[] unlockLevels;
    }

    [System.Serializable]
    public sealed class UnlockLevel
    {
        [field: SerializeField] private UnlockItem[] unlockItems;
        [field: SerializeField] private UnlockEnemy[] unlockEnemies;
    }

    [System.Serializable]
    public sealed class UnlockItem
    {
        [field: SerializeField] private ItemSO item;
        [field: SerializeField] private bool isUnlock;
    }

    [System.Serializable]
    public sealed class UnlockEnemy
    {
        [field: SerializeField] private EnemySO enemy;
        [field: SerializeField] private bool isUnlock;
    }
}