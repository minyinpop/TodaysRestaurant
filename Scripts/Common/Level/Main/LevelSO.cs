using Common.Level.Child.Level_Possible_Enemies;
using Common.Level.Child.Level_Possible_Items;
using Common.Scene_Name;
using UnityEngine;

namespace Common.Level.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Level/Main/Level", fileName = "New Data")]
    public sealed class LevelSO : ScriptableObject
    {
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
    
        [field: Header("Level Possible Items")]
        [field: SerializeField] private LevelIngredient levelIngredient;
                                public LevelIngredient LevelIngredient => levelIngredient;
                                
        [field: Header("Level Possible Enemies")]
        [field: SerializeField] private LevelEnemy levelEnemy;
                                public LevelEnemy LevelEnemy => levelEnemy;

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