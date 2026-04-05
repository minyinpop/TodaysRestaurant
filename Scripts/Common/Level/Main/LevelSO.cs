using Common.Level.Child;
using Common.Scene_Name;
using Common.Scene_Starter;
using UnityEngine;

namespace Common.Level.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Main/Level", fileName = "New Data")]
    public sealed class LevelSO : SceneStarterData
    {
        [field: Header("Level Name")]
        [field: SerializeField] private string levelName;
                                public string LevelName => levelName;
                                
        [field: Header("Level Scene Name")]
        [field: SerializeField] private SceneNameType terrainSceneNameType;
                                public SceneNameType TerrainSceneNameType => terrainSceneNameType;
        [field: SerializeField] private SceneNameType exploreSceneNameType;
                                public SceneNameType ExploreSceneNameType => exploreSceneNameType;
        [field: SerializeField] private SceneNameType battleSceneNameType;
                                public SceneNameType BattleSceneNameType => battleSceneNameType;
    
        [field: Header("Level Data")]
        [field: SerializeField] private LevelIngredientSO levelIngredientData;
                                public LevelIngredientSO LevelIngredientData => levelIngredientData;
        [field: SerializeField] private LevelEnemySO levelEnemyData;
                                public LevelEnemySO LevelEnemyData => levelEnemyData;

        private void OnValidate()
        {
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