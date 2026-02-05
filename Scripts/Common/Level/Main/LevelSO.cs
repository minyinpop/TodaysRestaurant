using Common.Level.Child.Level_Possible_Enemies;
using Common.Level.Child.Level_Possible_Items;
using UnityEngine;

namespace Common.Level.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Main/Level", fileName = "New Data")]
    public sealed class LevelSO : ScriptableObject
    {
        [field: Header("Level Name")]
        [field: SerializeField] private string levelName;
                                public string LevelName => levelName;
    
        [field: Header("Level Possible Items")]
        [field: SerializeField] private LevelPossibleItems levelPossibleItems;
                                public LevelPossibleItems LevelPossibleItems => levelPossibleItems;
                                
        [field: Header("Level Possible Enemies")]
        [field: SerializeField] private LevelPossibleEnemies levelPossibleEnemies;
                                public LevelPossibleEnemies LevelPossibleEnemies => levelPossibleEnemies;
    }
}