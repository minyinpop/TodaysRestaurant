using Common.Level.Child.Level_Enemy;
using Common.Level.Child.Level_Ingredient;
using Common.Scene_Name;
using UnityEngine;

namespace Common.Level.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Main/Level", fileName = "New Data")]
    public sealed class LevelSO : ScriptableObject
    {
        [field: Header("Level ID")]
        [field: SerializeField] private int levelID;
                                public int LevelID => levelID;
        
        [field: Header("Level Name")]
        [field: SerializeField] private string levelName;
                                public string LevelName => levelName;
                                
        [field: Header("Level Scene Name")]
        [field: SerializeField] private SceneNameSO terrainSceneNameData;
                                public SceneNameSO TerrainSceneNameData => terrainSceneNameData;
        [field: SerializeField] private SceneNameSO exploreSceneNameData;
                                public SceneNameSO ExploreSceneNameData => exploreSceneNameData;
        [field: SerializeField] private SceneNameSO battleSceneNameData;
                                public SceneNameSO BattleSceneNameData => battleSceneNameData;
    
        [field: Header("Level Data")]
        [field: SerializeField] private LevelIngredientSO levelIngredientData;
                                public LevelIngredientSO LevelIngredientData => levelIngredientData;
        [field: SerializeField] private LevelEnemySO levelEnemyData;
                                public LevelEnemySO LevelEnemyData => levelEnemyData;

        private void OnValidate()
        {
            if (terrainSceneNameData is null)
            {
                Debug.Log($"{GetType().Name} > {nameof(terrainSceneNameData)} cannot be null.");
            }

            if (exploreSceneNameData is null)
            {
                Debug.Log($"{GetType().Name} > {nameof(exploreSceneNameData)} cannot be null.");
            }
            
            if (battleSceneNameData is null)
            {
                Debug.Log($"{GetType().Name} > {nameof(battleSceneNameData)} cannot be null.");
            }

            if (levelIngredientData == null)
            {
                Debug.Log($"{GetType().Name} > {nameof(levelIngredientData)} cannot be null.");
            }

            if (levelEnemyData == null)
            {
                Debug.Log($"{GetType().Name} > {nameof(levelEnemyData)} cannot be null.");
            }
        }
    }
}