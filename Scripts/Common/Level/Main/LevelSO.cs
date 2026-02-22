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
        [field: SerializeField] private LevelIngredient levelIngredient;
                                public LevelIngredient LevelIngredient => levelIngredient;
                                
        [field: Header("Level Possible Enemies")]
        [field: SerializeField] private LevelEnemy levelEnemy;
                                public LevelEnemy LevelEnemy => levelEnemy;

        private void OnValidate()
        {
            if (levelIngredient == null)
            {
                Debug.Log($"{GetType().Name} > {nameof(levelIngredient)} cannot be null.");
            }

            if (levelEnemy == null)
            {
                Debug.Log($"{GetType().Name} > {nameof(levelEnemy)} cannot be null.");
            }
        }
    }
}