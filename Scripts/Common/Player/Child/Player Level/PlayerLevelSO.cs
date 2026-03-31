using Common.Level.Main;
using UnityEngine;

namespace Common.Player.Child.Player_Level
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Level", fileName = "New Data")]
    public sealed class PlayerLevelSO : ScriptableObject
    {
        [field: Header("Level")]
        [field: SerializeField] private LevelSO[] unlockLevels;
                                public LevelSO[] UnlockLevels => unlockLevels;
    }
}